using System;
using System.Collections.Generic;
using System.Diagnostics;
using CHUNITHM_Emulator.Config;
using CHUNITHM_Emulator.Control.WacomMT;
using static CHUNITHM_Emulator.Control.WacomMT.WacomMTAPI;

namespace CHUNITHM_Emulator.Control {
	/// <summary>
	/// ワコムタブレットのマルチタッチ機能のコントローラー
	/// </summary>
	class WacomMTController {

		#region Peivate Field & Property

		/// <summary>
		/// デバイスが利用可能か
		/// </summary>
		private bool isDeviceAvailable = false;

		/// <summary>
		/// コールバック関数
		/// </summary>
		private WMT_FINGER_CALLBACK callback;

		/// <summary>
		/// アクティブなデバイスのインデックス
		/// </summary>
		private int activeDevice = 0;

		/// <summary>
		/// デバイスのid
		/// </summary>
		private int[] deviceIds;

		/// <summary>
		/// デバイスの情報
		/// </summary>
		private WacomMTCapability[] deviceCapabilitys;

		/// <summary>
		/// 指のリスト
		/// </summary>
		private Dictionary<int, FingerState> fingers = new Dictionary<int, FingerState>();

		#endregion

		#region Internal Field & Property 

		/// <summary>
		/// パネルとAirの状態を取得します。
		/// </summary>
		internal (short top, short under, int airLevel) ControllerState {
			get {
				if (!this.isDeviceAvailable) {
					return (0, 0, -1);
				}
				int top = 0, under = 0, airLevel = 0;
				lock (this.fingers) {
					foreach (FingerState finger in this.fingers.Values) {
						top = (short)top | finger.TopPanels;
						under = (short)under | finger.UnderPanels;
						airLevel = (airLevel < finger.AirLevel ? finger.AirLevel : airLevel);
					}
				}
				return ((short)top, (short)under, airLevel);
			}
		}

		#endregion

		/// <summary>
		/// 初期化
		/// </summary>
		internal void Initialize() {
			Trace.WriteLine("Initialze WacomMTController.");
			WacomMTError ret;
			try {
				ret = WacomMTInitialize(); //ライブラリの初期化
				if (ret != WacomMTError.WMTErrorSuccess) {
					//初期化に失敗した場合
					Trace.WriteLine("Faild : Initialize WacomMT Library.");
					return;
				}
			} catch (DllNotFoundException) {
				//DLLが見つからなかった場合
				Trace.WriteLine("Dll Not found : " + "Wacom feel Multi-touch (" + DllName + ").");
				return;
			}

			Trace.WriteLine("Available of WacomMT Device.");

			this.deviceIds = WacomMTGetAttachedDeviceIDs(); //デバイスの取得
			if (this.deviceIds.Length <= 0) {
				//デバイスが1つも見つからなかったら
				Trace.WriteLine("Device Not Found.");
				return;
			}

			this.deviceCapabilitys = new WacomMTCapability[this.deviceIds.Length];
			for (int i = 0; i < this.deviceIds.Length; i++) {
				//デバイスの情報を取得
				this.deviceCapabilitys[i] = WacomMTGetDeviceCapabilities(i);
				Trace.WriteLine("Device ID:" + this.deviceIds[i]);
			}


			//コールバック関数を設定 
			this.callback = this.FingerCallback;
			WacomMTRegisterFingerReadCallback(this.deviceIds[this.activeDevice], IntPtr.Zero, WacomMTProcessingMode.WMTProcessingModeNone, this.callback, IntPtr.Zero);

			this.isDeviceAvailable = true;

		}


		/// <summary>
		/// 終了処理
		/// </summary>
		internal void Terminate() {
			if (this.isDeviceAvailable) {
				WacomMTUnRegisterBlobReadCallback(this.deviceIds[this.activeDevice], IntPtr.Zero, WacomMTProcessingMode.WMTProcessingModeNone, IntPtr.Zero);
				WacomMTQuit();
				this.callback = null;
			}
		}


		/// <summary>
		/// WMTのコールバック関数
		/// </summary>
		/// <param name="fingerPacket">指情報</param>
		/// <param name="userData"><see cref="IntPtr.Zero"/></param>
		/// <returns>0</returns>
		private int FingerCallback(IntPtr fingerPacket, IntPtr userData) {
			WacomMTFingerCollectionClass fingerCollection = new WacomMTFingerCollectionClass(fingerPacket);
			WacomMTCapability capability = this.deviceCapabilitys[this.activeDevice];
			foreach (WacomMTFinger finger in fingerCollection.FingerData) {
				switch (finger.TouchState) {
					case WacomMTFingerState.WMTFingerStateNone:
						break;
					case WacomMTFingerState.WMTFingerStateDown: {
							FingerState state = new FingerState(finger.FingerID, finger.TouchState);
							state.SetPosition(finger.X, finger.Y, finger.Width, finger.Height, capability.LogicalWidth, capability.LogicalHeight);
							lock (this.fingers) {
								this.fingers[state.ID] = state;
							}
							//Trace.WriteLine("Touch #" + state.ID + " Position:" + state.UnderPanels + " AirLevel:" + state.AirLevel);

							break;
						}
					case WacomMTFingerState.WMTFingerStateHold: {
							lock (this.fingers) {
								FingerState state = this.fingers.ContainsKey(finger.FingerID) ? this.fingers[finger.FingerID] : null;
								if (state == null) {
									state = new FingerState(finger.FingerID, finger.TouchState);
									state.SetPosition(finger.X, finger.Y, finger.Width, finger.Height, capability.LogicalWidth, capability.LogicalHeight);
									this.fingers[state.ID] = state;
									break;
								}
								state.State = finger.TouchState;
								state.SetPosition(finger.X, finger.Y, finger.Width, finger.Height, capability.LogicalWidth, capability.LogicalHeight);
							}
							break;
						}
					case WacomMTFingerState.WMTFingerStateUp: {
							lock (this.fingers) {
								this.fingers.Remove(finger.FingerID);
							}
							break;
						}
				}
			}
			return 0;
		}

		/// <summary>
		/// 指のステータス
		/// </summary>
		internal class FingerState {

			/// <summary>
			/// 指ID
			/// </summary>
			internal readonly int ID;

			/// <summary>
			/// 指の状態
			/// </summary>
			internal WacomMTFingerState State;

			/// <summary>
			/// パネル上部の位置(ビット表現)
			/// </summary>
			internal short TopPanels { private set; get; }

			/// <summary>
			/// パネル下部の位置(ビット表現)
			/// </summary>
			internal short UnderPanels { private set; get; }

			/// <summary>
			/// Airの位置
			/// </summary>
			internal int AirLevel = -1;

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="id">指ID</param>
			/// <param name="state">状態</param>
			internal FingerState(int id, WacomMTFingerState state) {
				this.ID = id;
				this.State = state;
			}

			/// <summary>
			/// 指の位置をパネルまたはAirの位置情報に変換し、格納します。
			/// </summary>
			/// <param name="x">指のX座標</param>
			/// <param name="y">指のY座標</param>
			/// <param name="width">接触面の幅</param>
			/// <param name="height">接触面の高さ</param>
			/// <param name="logicalWidth">読み取り可能領域の幅</param>
			/// <param name="logicalHeight">読み取り可能領域の高さ</param>
			internal void SetPosition(float x, float y, float width, float height, float logicalWidth, float logicalHeight) {

				this.TopPanels = this.UnderPanels = 0;
				this.AirLevel = -1;

				int min = (int)((x - (width / 2)) / logicalWidth * 16);
				int max = (int)((x + (width / 2)) / logicalWidth * 16);
				int level = 0;
				if (MainConfig.Instance.AirMode == AirMode.Manual) {
					level = (int)(y / logicalHeight * 3);
				} else {
					level = (int)(y / logicalHeight * 2) + 1;
				}

				for (int i = min; i <= max; i++) {
					switch (level) {
						case 0:
							this.AirLevel = (100 - (int)(y / (logicalHeight / 3) * 100));
							break;
						case 1:
							this.TopPanels = (short)(this.TopPanels | (short)(1 << i));
							break;
						case 2:
							this.UnderPanels = (short)(this.UnderPanels | (short)(1 << i));
							break;
					}
				}

			}

		}


	}
}
