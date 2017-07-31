using System;
using CHUNITHM_Emulator.Config;
using CHUNITHM_Emulator.Renderer;
using static DxLibDLL.DX;

namespace CHUNITHM_Emulator.Chunithm {
	/// <summary>
	/// CHUNITHM
	/// </summary>
	internal class CHUNITHM {

		#region Static Field & Property

		/// <summary>
		/// インスタンス
		/// </summary>
		internal static CHUNITHM Instance { private set; get; }

		#endregion

		#region Internal Field & Property

		/// <summary>
		/// レンダリングクラス
		/// </summary>
		internal RenderringEngine Renderer { private set; get; }

		/// <summary>
		/// システムのプロパティ
		/// </summary>
		internal SystemProperties Properties { private set; get; }

		/// <summary>
		/// Controller
		/// </summary>
		internal Controller Controller { private set; get; }

		#endregion

		#region Static Method

		/// <summary>
		/// クラスの初期化
		/// </summary>
		internal static void Initialize() {
			if (Instance != null) {
				return; //既に初期化されていたら処理しない
			}

			Instance = new CHUNITHM();
		}

		#endregion


		#region private Method

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private CHUNITHM() {
			Instance = this;
			this.Renderer = new RenderringEngine();
			int resolution = MainConfig.Instance.Resolution / 9;
			this.Properties = new SystemProperties() {
				Hight = resolution * 9,
				Width = resolution * 16
			};
			this.Controller = new Controller();
		}

		#endregion

		#region internal Method

		internal void Run() {


			//内部Tickの初期化
			this.Properties.NowSystemTick = Environment.TickCount;
			this.Properties.OldSystemTick = this.Properties.NowSystemTick;

			int calc = 0;
			int flameCount = 0; 

			while (ProcessMessage() == 0) {
				// 現在の内部TickのOld代入および現在Tickの取得
				this.Properties.OldSystemTick = this.Properties.NowSystemTick;
				this.Properties.NowSystemTick = Environment.TickCount;
				
				int delta = this.Properties.NowSystemTick - this.Properties.OldSystemTick;//1フレームにかかった時間の計測(ミリ秒)

				calc += delta;
				flameCount++;

				if (calc >= 1000) {
					this.Properties.FPS = flameCount;
					calc = flameCount = 0;
				}

				this.Controller.ControllerStateUpdate();

				this.Renderer.Draw();
			}

		}


		#endregion


	}
}
