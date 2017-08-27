using System;
using System.Drawing;
using CHUNITHM_Emulator.Chunithm;
using CHUNITHM_Emulator.Renderer.Object;
using static CHUNITHM_Emulator.Util.DxUtils;
using static DxLibDLL.DX;

namespace CHUNITHM_Emulator.Renderer.Scene {
	/// <summary>
	/// スタート画面のシーン
	/// </summary>
	internal class StartScene : IScene {

		#region const

		/// <summary>
		/// スタート画面の背景画像
		/// </summary>
		private const string BackGroundSkin = SystemProperties.SkinLocation + @"START_BACKGROUND.png";

		/// <summary>
		/// スタート画面のオーバーレイ画像
		/// </summary>
		private const string OverSkin = SystemProperties.SkinLocation + @"START_OVER.png";

		/// <summary>
		/// スタート画面の海
		/// </summary>
		private const string Sea = SystemProperties.SkinLocation + @"START_SEA_";

		/// <summary>
		/// ロゴマークの画像
		/// </summary>
		private const string Logo = SystemProperties.SkinLocation + @"START_LOGO.png";

		/// <summary>
		/// 飛んでるオブジェクトの画像
		/// </summary>
		private const string Object = SystemProperties.SkinLocation + @"TITLE_OBJECT.png";

		#endregion

		private int flame = 0;

		/// <summary>
		/// テクスチャのハンドル
		/// </summary>
		private readonly int[] textureHandls = new int[7];

		/// <summary>
		/// 海0の描画オブジェクト
		/// </summary>
		private readonly Square[] seaRenderer = new Square[3];

		/// <summary>
		/// ロゴマーク
		/// </summary>
		private readonly Square logo;

		/// <summary>
		///	星型
		/// </summary>
		private readonly Square[] star = new Square[30];

		/// <summary>
		/// ハート型
		/// </summary>
		private readonly Square[] heart = new Square[30];

		/// <summary>
		/// 三角
		/// </summary>
		private readonly Square[] triangle = new Square[30];


		/// <summary>
		/// コンストラクタ
		/// </summary>
		internal StartScene() {
			this.textureHandls[0] = LoadGraph(BackGroundSkin);
			this.textureHandls[1] = LoadGraph(OverSkin);
			this.textureHandls[2] = LoadGraph(Sea + "0.png");
			this.textureHandls[3] = LoadGraph(Sea + "1.png");
			this.textureHandls[4] = LoadGraph(Sea + "2.png");
			this.textureHandls[5] = LoadGraph(Logo);
			this.textureHandls[6] = LoadGraph(Object);

			int width = CHUNITHM.Instance.Properties.Width;
			int hight = CHUNITHM.Instance.Properties.Hight;

			this.seaRenderer[0] = new Square(-width, -hight / 2, 0.0F, width * 3, hight * 2, this.textureHandls[2], true);
			this.seaRenderer[0].RotationX(GetRadian(75));
			this.seaRenderer[0].Reflect();

			this.seaRenderer[1] = new Square(-width, 0.0F, 0.0F, width * 3, hight * 3, this.textureHandls[3], true);
			this.seaRenderer[1].SetTexturePositions(0.0F, 0.0F, 3.0F, 0.0F, 0.0F, 3.0F, 3.0F, 3.0F);
			this.seaRenderer[1].RotationX(GetRadian(75.0F));

			this.seaRenderer[2] = new Square(-width, 0.0F, 0.0F, width * 3, hight * 3, Color.FromArgb(100, 255, 255, 255), this.textureHandls[4], true);
			this.seaRenderer[2].SetTexturePositions(0.0F, 0.0F, 3.0F, 0.0F, 0.0F, 3.0F, 3.0F, 3.0F);
			this.seaRenderer[2].RotationX(GetRadian(75.0F));

			int width_logo = width / 8 * 5;
			int hight_logo = hight / 3;

			this.logo = new Square(width / 2 - (width_logo / 2), hight / 2 - (hight_logo / 2), 0, width_logo, hight_logo, this.textureHandls[5], true);

			for (int i = 0; i < 30; i++) {
				this.star[i] = new Square(0, 0, 0, 125, 125, Color.Red, this.textureHandls[6], true);
				this.star[i].SetTexturePositions(0F, 0F, 1F / 3F, 0F, 0F, 1F, 3F / 1F, 1F);
				this.heart[i] = new Square(0, 0, 0, 125, 125, Color.Red, this.textureHandls[6], true);
				this.heart[i].SetTexturePositions(1F / 3F, 0F, 2F / 3F, 0F, 1F / 3F, 1F, 3F / 2F, 1F);
				this.triangle[i] = new Square(0, 0, 0, 125, 125, Color.Red, this.textureHandls[6], true);
				this.triangle[i].SetTexturePositions(2F / 3F, 0F, 1F, 0F, 2F / 3F, 1F, 1F, 1F);
			}

			return;
		}

		/// <summary>
		/// Tick処理
		/// </summary>
		/// <param name="flameSec">前回のフレームからの経過時間</param>
		public void TickUpdate(int flameSec) => this.flame++;

		/// <summary>
		/// 描画
		/// </summary>
		public void DrawScene() {

			SetFogEnable(TRUE);
			SetFogColor(255, 255, 255);
			SetFogStartEnd(1000, 2000);

			int width = CHUNITHM.Instance.Properties.Width;
			int hight = CHUNITHM.Instance.Properties.Hight;
			DrawModiGraph(0, 0, width, 0, width, hight, 0, hight, this.textureHandls[0], FALSE);

			SetUseLighting(FALSE);


			SetDrawArea(0, hight / 20 * 11, width, hight);
			this.seaRenderer[0].Draw();
			SetTextureAddressModeUV(DX_TEXADDRESS_WRAP, DX_TEXADDRESS_WRAP);
			this.seaRenderer[1].TranslateBefore(0.0F, -0.5F, 0.0F);
			this.seaRenderer[2].TranslateBefore(0.0F, -0.5F, 0.0F);
			if (this.flame % (hight * 2) == 0) {
				this.seaRenderer[1].TranslateBefore(0.0F, hight, 0.0F);
				this.seaRenderer[2].TranslateBefore(0.0F, hight, 0.0F);
			}
			this.seaRenderer[1].Draw();
			this.seaRenderer[2].Draw();

			SetTextureAddressModeUV(DX_TEXADDRESS_CLAMP, DX_TEXADDRESS_CLAMP);
			SetDrawAreaFull();

			this.logo.Draw();

			SetFogEnable(FALSE);

			this.triangle[0].Draw();

			SetUseLighting(TRUE);

			DrawModiGraph(0, 0, width, 0, width, hight, 0, hight, this.textureHandls[1], TRUE);

		}

		/// <summary>
		/// シーンのリソースを破棄
		/// </summary>
		public void Dispose() {
			foreach (int handle in this.textureHandls) {
				DeleteGraph(handle);
			}
			return;
		}

	}
}
