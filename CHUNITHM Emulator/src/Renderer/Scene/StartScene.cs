using System.Drawing;
using CHUNITHM_Emulator.Chunithm;
using static CHUNITHM_Emulator.Util.DxUtils;
using static DxLibDLL.DX;

namespace CHUNITHM_Emulator.Renderer.Scene {
	/// <summary>
	/// スタート画面のシーン
	/// </summary>
	internal class StartScene : IScene {

		private const string BackGroundSkin = SystemProperties.SkinLocation + @"START_BACKGROUND.png";

		private readonly int backGroundSkinHandle;

		internal StartScene() {
			this.backGroundSkinHandle = LoadGraph(BackGroundSkin);
			return;
		}

		/// <summary>
		/// Tick処理
		/// </summary>
		/// <param name="flameSec">前回のフレームからの経過時間</param>
		public void TickUpdate(int flameSec) {
		}

		/// <summary>
		/// 描画
		/// </summary>
		public void DrawScene() {

			int width = CHUNITHM.Instance.Properties.Width;
			int hight = CHUNITHM.Instance.Properties.Hight;
			DrawModiGraph(0, 0, width, 0, width, hight, 0, hight, this.backGroundSkinHandle, FALSE);
			//DrawLine3D(new VECTOR { x = 100, y = 100, z = -100}, new VECTOR { x = 500, y = 500, z = 0 }, (uint)Color.White.ToArgb());

		}

		/// <summary>
		/// シーンのリソースを破棄
		/// </summary>
		public void Dispose() {
			DeleteGraph(this.backGroundSkinHandle);
			return;
		}

	}
}
