using System;
using CHUNITHM_Emulator.Chunithm;
using CHUNITHM_Emulator.Config;
using static DxLibDLL.DX;

namespace CHUNITHM_Emulator.Renderer {
	/// <summary>
	/// レンダリングクラス
	/// </summary>
	internal class RenderringEngine {

		/// <summary>
		/// システムプロパティの描画色
		/// </summary>
		private const uint SystemPropertyColor = 0xFFFFFFFF;

		#region internal Method

		/// <summary>
		/// コンストラクタ
		/// </summary>
		internal RenderringEngine() {

		}

		/// <summary>
		/// 描画
		/// </summary>
		internal void Draw() {
			ClearDrawScreen();//クリア

			if (MainConfig.Instance.IsDrawSystemProperties) {
				this.DrawSystemProperty();
			}

			ScreenFlip();//表に反映
		}

		#endregion

		#region private Method

		private void DrawSystemProperty() {
			ChangeFont(MainConfig.Instance.SystemPropertyFont);
			ChangeFontType(DX_FONTTYPE_ANTIALIASING_EDGE);
			#region 左側
			DrawString(0, 0, "CHUNITHM Emulator " + ChunithmEmuMain.VERSION, SystemPropertyColor);
			DrawString(0, 15, "FPS : " + CHUNITHM.Instance.Properties.FPS.ToString(), SystemPropertyColor);
			{
				char[] chars = Convert.ToString(CHUNITHM.Instance.Controller.PanelState, 2).ToCharArray();
				Array.Reverse(chars);
				string text = new string(chars);
				for (int i = text.Length; i < 16; i++) {
					text += "0";
				}
				string text2 = "";
				foreach (char c in text.ToCharArray()) {
					text2 += (c == '0' ? "□" : "■");
				}
				DrawString(0, 30, "PanelState:" + text2, SystemPropertyColor);

				chars = Convert.ToString(CHUNITHM.Instance.Controller.PanelHoldState, 2).ToCharArray();
				Array.Reverse(chars);
				text = new string(chars);
				for (int i = text.Length; i < 16; i++) {
					text += "0";
				}
				text2 = "";
				foreach (char c in text.ToCharArray()) {
					text2 += (c == '0' ? "□" : "■");
				}
				DrawString(0, 45, "PanelHoldState:" + text2, SystemPropertyColor);
			}
			DrawString(0, 60, "AirLevel:" + CHUNITHM.Instance.Controller.AirLevel.ToString("000") + " AirState:" + CHUNITHM.Instance.Controller.AirState, SystemPropertyColor);
			#endregion
			#region 右側
			{
				string text = "CommonLanguageRuntime:" + CHUNITHM.Instance.Properties.CLRVersion + " " + (System.Environment.Is64BitProcess ? "64bit" : "32bit");
				DrawString(CHUNITHM.Instance.Properties.Width - GetDrawStringWidth(text, GetStringLength(text)), 0, text, SystemPropertyColor);
				text = "Build CommonLanguageRuntime:" + CHUNITHM.Instance.Properties.BuildCLRVersion;
				DrawString(CHUNITHM.Instance.Properties.Width - GetDrawStringWidth(text, GetStringLength(text)), 15, text, SystemPropertyColor);
				text = "Mem: " + (Environment.WorkingSet / 1048576.0).ToString("#0.00") + "MB";
				DrawString(CHUNITHM.Instance.Properties.Width - GetDrawStringWidth(text, GetStringLength(text)), 30, text, SystemPropertyColor);
			}
			#endregion

		}

		#endregion

	}
}
