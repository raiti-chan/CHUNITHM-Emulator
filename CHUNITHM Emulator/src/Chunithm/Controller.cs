using static DxLibDLL.DX;

namespace CHUNITHM_Emulator.Chunithm {
	internal class Controller {

		#region Internal static property

		/// <summary>
		/// パネル下部分のキー配列
		/// </summary>
		internal static readonly int[] UnderKeys = { KEY_INPUT_A, KEY_INPUT_Z, KEY_INPUT_S, KEY_INPUT_X, KEY_INPUT_D, KEY_INPUT_C, KEY_INPUT_F, KEY_INPUT_V, KEY_INPUT_G, KEY_INPUT_B, KEY_INPUT_H, KEY_INPUT_N, KEY_INPUT_J, KEY_INPUT_M, KEY_INPUT_K, KEY_INPUT_COMMA };

		/// <summary>
		/// パネル上部分のキー配列
		/// </summary>
		internal static readonly int[] TopKeys = { KEY_INPUT_1, KEY_INPUT_Q, KEY_INPUT_2, KEY_INPUT_W, KEY_INPUT_3, KEY_INPUT_E, KEY_INPUT_4, KEY_INPUT_R, KEY_INPUT_5, KEY_INPUT_T, KEY_INPUT_6, KEY_INPUT_Y, KEY_INPUT_7, KEY_INPUT_U, KEY_INPUT_8, KEY_INPUT_I };

		#endregion

		#region Private property

		/// <summary>
		/// 現在のキーボードの状態
		/// </summary>
		private byte[] keybordCurrent = new byte[256];

		/// <summary>
		/// 過去のキーボードの状態
		/// </summary>
		private byte[] keybordLast = new byte[256];

		/// <summary>
		/// キーボードの状態が切り替わったか
		/// </summary>
		private byte[] keybordPush = new byte[256];

		/// <summary>
		/// 現在のパネルの状態
		/// </summary>
		private short panelCurrent = 0;

		/// <summary>
		/// 過去のパネルの状態
		/// </summary>
		private short panelLast = 0;

		/// <summary>
		/// パネルの状態が切り替わったか
		/// </summary>
		private short panelPush = 0;

		#endregion

		#region Internal property

		/// <summary>
		/// パネルの状態を表します。
		/// </summary>
		internal short PanelState { get => this.panelPush; }

		/// <summary>
		/// パネルのホールド状態を表します。
		/// </summary>
		internal short PanelHoldState { get => this.panelLast; }

		#endregion

		/// <summary>
		/// Air判定の状態を表します。
		/// </summary>
		internal AirStates AirState { private set; get; } = 0;

		/// <summary>
		/// Controllerの状態を更新します
		/// </summary>
		internal void ControllerStateUpdate() {
			this.keybordCurrent.CopyTo(this.keybordLast, 0); //過去のバッファに現在の状態をコピー
			GetHitKeyStateAll(this.keybordCurrent); //キーの状態を取得

			for (int i = 0; i < 256; i++) {
				this.keybordPush[i] = (byte)(~this.keybordLast[i] & this.keybordCurrent[i]);
			}

			this.panelLast = this.panelCurrent; //過去のバッファに現在の状態をコピー
			this.panelCurrent = 0;//現在の状態を0に
			for (int i = 0; i < 16; i++) {
				this.panelCurrent = (short)(this.panelCurrent | ((short)(this.keybordCurrent[UnderKeys[i]] << i)));
			}

			// TODO: タブレットの処理

			this.panelPush = (short)(~this.panelLast & this.panelCurrent);

		}



		/// <summary>
		/// Airの状態を取得します。
		/// </summary>
		internal enum AirStates : byte {
			/// <summary>
			/// Air判定が無い状態
			/// </summary>
			Low,
			/// <summary>
			/// Air判定がある状態
			/// </summary>
			High,
			/// <summary>
			/// AirAction判定
			/// </summary>
			Action
		}

	}
}
