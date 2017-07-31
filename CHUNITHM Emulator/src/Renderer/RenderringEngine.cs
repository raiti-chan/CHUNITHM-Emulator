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
			#endregion
			#region 右側
			string text = "CommonLanguageRuntime:" + CHUNITHM.Instance.Properties.CLRVersion + " " + (System.Environment.Is64BitProcess ? "64bit" : "32bit");
			DrawString(CHUNITHM.Instance.Properties.Width - GetDrawStringWidth(text, GetStringLength(text)), 0, text, SystemPropertyColor);
			text = "Build CommonLanguageRuntime:" + CHUNITHM.Instance.Properties.BuildCLRVersion;
			DrawString(CHUNITHM.Instance.Properties.Width - GetDrawStringWidth(text, GetStringLength(text)), 15, text, SystemPropertyColor);
			text = "Mem: " + (Environment.WorkingSet/1048576.0).ToString("#0.00") + "MB";
			DrawString(CHUNITHM.Instance.Properties.Width - GetDrawStringWidth(text, GetStringLength(text)), 30, text, SystemPropertyColor);
			#endregion

		}

		#endregion

	}
}
