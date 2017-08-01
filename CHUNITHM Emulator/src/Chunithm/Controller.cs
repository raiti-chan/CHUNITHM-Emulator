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
		/// 現在のパネル下部の状態
		/// </summary>
		private short panelUnderCurrent = 0;

		/// <summary>
		/// 現在のパネル上部の状態
		/// </summary>
		private short panelTopCurrent = 0;

		/// <summary>
		/// 過去のパネル下部の状態
		/// </summary>
		private short panelUnderLast = 0;

		/// <summary>
		/// 現在のパネル上部の状態
		/// </summary>
		private short panelTopLast = 0;

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
		internal short PanelHoldState { get => (short)(this.panelUnderLast | this.panelTopCurrent); }

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
				this.keybordPush[i] = (byte)(~this.keybordLast[i] & this.keybordCurrent[i]);//押された状態に切り替わったキーを取得
			}

			this.panelUnderLast = this.panelUnderCurrent; //過去のバッファに現在の状態をコピー
			this.panelTopLast = this.panelTopCurrent;

			this.panelUnderCurrent = this.panelTopCurrent = 0; //現在の状態を0に
			for (int i = 0; i < 16; i++) {
				this.panelUnderCurrent = (short)(this.panelUnderCurrent | ((short)(this.keybordCurrent[UnderKeys[i]] << i))); //パネル下部の状態を取得
				this.panelTopCurrent = (short)(this.panelTopCurrent | ((short)(this.keybordCurrent[TopKeys[i]] << i))); //パネル上部の状態を取得
			}

			// TODO: タブレットの処理

			this.panelPush = (short)((~this.panelUnderLast & this.panelUnderCurrent) | (~this.panelTopLast & this.panelTopCurrent));

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
