using System;
using System.Diagnostics;
using System.Windows.Forms;
using CHUNITHM_Emulator.Chunithm.Enums;
using CHUNITHM_Emulator.Config;
using CHUNITHM_Emulator.Control;
using CHUNITHM_Emulator.Dialog;
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


		#region private
		/// <summary>
		/// 終了するかのフラグ
		/// </summary>
		private bool isExit = false;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private CHUNITHM() {
			Instance = this;
			this.Renderer = new RenderringEngine();
			int resolution = MainConfig.Instance.Resolution / 16;
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

		/// <summary>
		/// ウィンドウの設定を変更します。
		/// </summary>
		private void WindowSetting() {
			int width = MainConfig.Instance.Resolution;
			bool isFullScreen = MainConfig.Instance.IsFullScreen;
			WindowModeDialog dialog = new WindowModeDialog(width, width / 16 * 9, isFullScreen);
			Trace.WriteLine("Show WindowSettingDialog.");
			DialogResult result = dialog.ShowDialog();

			if (result == DialogResult.Cancel) {
				//キャンセルが押された場合何もしない
				Trace.WriteLine("Cancel.");
				return;
			}

			width = dialog.ResolutionWidth;
			isFullScreen = dialog.IsFullScreen;
			Trace.WriteLine("Chenge.");
			Trace.WriteLine("Resolution:" + width + "×" + width / 16 * 9);
			Trace.WriteLine("IsFullScreen:" + isFullScreen);

			MainConfig.Instance.Resolution = width;
			MainConfig.Instance.IsFullScreen = isFullScreen;

			result = MessageBox.Show("変更された設定はアプリケーション再起動後に適用されます。\n再起動しますか?",
				"設定が変更されました", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (result == DialogResult.No) {
				return;
			}

			this.Restert();

			return;
		}

		#endregion

		#region internal Method

		/// <summary>
		/// CHUNITHMの起動
		/// </summary>
		internal void Run() {

			Trace.WriteLine("Run CHUNITHM.");

			this.Properties.GameState = GameState.Start;//ゲームの状態をスタート画面に
			this.Properties.GameScene = new StartScene();//スタート画面のシーンを設定

			//内部Tickの初期化
			this.Properties.NowSystemTick = Environment.TickCount;
			this.Properties.OldSystemTick = this.Properties.NowSystemTick;

			int calc = 0;
			int flameCount = 0;

			while (ProcessMessage() == 0 && !this.isExit) {
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
					//F3が押されたらデバッグ情報の表示の切り替え
					MainConfig.Instance.IsDrawSystemProperties = MainConfig.Instance.IsDrawSystemProperties ? false : true;
					Trace.WriteLine("IsDrawProperties:" + MainConfig.Instance.IsDrawSystemProperties);
				}

				if (this.Controller.GetKeyCurrent(KEY_INPUT_LCONTROL) && this.Controller.GetKeyPush(KEY_INPUT_F1)) {
					//LCtrl + F1が押されたら画面サイズなどの設定
					Trace.WriteLine("WindowSetting.");
					WindowSetting();
				}
				this.TickUpdate(delta);
				this.Renderer.Draw();
				this.Properties.Flames++; //経過フレームを増加
			}

		}

		/// <summary>
		/// CHUNITHMを終了できるか検証し、できるならば終了します。
		/// </summary>
		internal bool Exit() {

			Trace.WriteLine("CanExit:" + true);
			this.isExit = true;
			return true;
		}

		/// <summary>
		/// アプリケーションを再起動します。
		/// </summary>
		/// <returns>再起動できた場合true</returns>
		internal bool Restert() {
			Trace.WriteLine("Restarting.");
			bool canExit = Exit();
			if (!canExit) {
				Trace.WriteLine("Can't Exit.");
				MessageBox.Show("アプリケーションの終了に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

				return false;
			}

			int processId = Process.GetCurrentProcess().Id;
			string cmd = "\"" + processId.ToString() + "\" \"30000\" " + Environment.CommandLine;
			string exePath = "Restarter.exe";
			Trace.WriteLine("ProcessStart:" + exePath + " " + cmd);
			Process.Start("Restarter.exe", cmd);


			return true;
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
