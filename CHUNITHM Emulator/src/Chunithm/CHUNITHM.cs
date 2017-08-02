using System;
using System.Diagnostics;
using CHUNITHM_Emulator.Chunithm.Enums;
using CHUNITHM_Emulator.Config;
using CHUNITHM_Emulator.Control;
using CHUNITHM_Emulator.Renderer;
using CHUNITHM_Emulator.Renderer.Scene;
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
			Trace.WriteLine("Init Chunithm.");

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

		/// <summary>
		/// ゲームを更新します。
		/// </summary>
		/// <param name="flameSec">前回のフレームからの経過時間</param>
		private void TickUpdate(int flameSec) {
			this.Properties.GameScene.TickUpdate(flameSec);
			return;
		}

		#endregion

		#region internal Method

		internal void Run() {

			Trace.WriteLine("Run CHUNITHM.");

			this.Properties.GameState = GameState.Start;//ゲームの状態をスタート画面に
			this.Properties.GameScene = new StartScene();//スタート画面のシーンを設定

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

				this.Controller.ControllerStateUpdate();//コントローラーの状態の更新

				if (this.Controller.GetKeyPush(KEY_INPUT_F3)) {
					MainConfig.Instance.IsDrawSystemProperties = MainConfig.Instance.IsDrawSystemProperties ? false : true;
				}
				this.TickUpdate(delta);
				this.Renderer.Draw();
				this.Properties.Flames++; //経過フレームを増加
			}

		}

		/// <summary>
		/// 終了処理
		/// </summary>
		internal void Terminate() {
			this.Controller.Terminate();
			this.Controller = null;
			this.Properties.GameScene.Dispose();
			MainConfig.Instance.SaveJson();
		}

		#endregion


	}
}
