using System;
using System.Diagnostics;
using System.Windows.Forms;
using CHUNITHM_Emulator.Chunithm;
using CHUNITHM_Emulator.Config;
using CHUNITHM_Emulator.Dialog;
using CHUNITHM_Emulator.Logger;
using static DxLibDLL.DX;


namespace CHUNITHM_Emulator {
	/// <summary>
	/// アプリケーションの起動クラス
	/// </summary>
	internal class ChunithmEmuMain {

		/// <summary>
		/// このアプリケーション名
		/// </summary>
		internal const string NAME = "CHUNITHM Emulator";

		/// <summary>
		/// このアプリケーションバージョン
		/// </summary>
		internal const string VERSION = "0.0.0α";

		/// <summary>
		/// 起動メソッド
		/// </summary>
		[STAThread]
		private static void Main() {

			PreInitialize();

			int ret = Initialize();

			if (ret != 0) {
				switch (ret) {
					case -1:
						Trace.WriteLine("Error : DXLib Init");
						MessageBox.Show("DXライブラリの初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
				}
			}

			CHUNITHM.Instance.Run();

			Trace.WriteLine("Finalize!");
			CHUNITHM.Instance.Terminate();

			DxLib_End();
			Trace.WriteLine("Exit");
		}

		/// <summary>
		/// 一番最初に行われるべき初期化処理
		/// </summary>
		private static void PreInitialize() {

			Trace.Listeners.Clear();
			Trace.Listeners.Add(new LogTraceListerToFile());
			Trace.WriteLine("PreInit!!");

			//初回起動かチェック
			bool isFirst = Properties.Settings.Default.IsFirst;
			if (isFirst) {
				//初回起動だったら画面設定をする
				Trace.WriteLine("The first time.");
				WindowModeDialog dialog = new WindowModeDialog();
				Trace.WriteLine("Show WindowSettingDialog.");
				dialog.ShowDialog();
				MainConfig.Instance.IsFullScreen = dialog.IsFullScreen;
				MainConfig.Instance.Resolution = dialog.ResolutionWidth;

				Properties.Settings.Default.IsFirst = false;
				Properties.Settings.Default.Save();
			}

			Trace.WriteLine("IsFullScreen:" + MainConfig.Instance.IsFullScreen);
			ChangeWindowMode(MainConfig.Instance.IsFullScreen ? FALSE : TRUE);//ウィンドウモードに変更

			SetMainWindowText(NAME + " Ver-" + VERSION);//ウィンドウ名の取得
			SetAlwaysRunFlag(TRUE);//バックグラウンドでも処理を続けるように

			Trace.WriteLine("UseASyncLoadFlag:" + MainConfig.Instance.UseASyncLoad);
			SetUseASyncLoadFlag(MainConfig.Instance.UseASyncLoad ? TRUE : FALSE);

			// SetWindowIconHandle アイコンの設定
			SetUseFPUPreserveFlag(TRUE);//D3DCREATE_FPU_PRESERVEを使うように

			Trace.WriteLine("Resolution:" + MainConfig.Instance.Resolution + "×" + MainConfig.Instance.Resolution / 16 * 9);
			int resolution = MainConfig.Instance.Resolution / 16;
			SetGraphMode(resolution * 16, resolution * 9, 32);//グラフィックモードの設定(X,Y,ColorBits)(ウィンドウサイズも連動)

			Trace.WriteLine("FullSceneAntiAliasingMode:Samples=" + MainConfig.Instance.AntiAliasSamples +
				" Quality=" + MainConfig.Instance.Resolution / 16 * 9);
			SetFullSceneAntiAliasingMode(MainConfig.Instance.AntiAliasSamples, MainConfig.Instance.AntiAliasQuality);//アンチエイリアス(解像度倍率,クオリティー0~3)

		}

		/// <summary>
		/// DXライブラリの初期化や設定、アプリケーションの初期化など。
		/// </summary>
		/// <returns>成功の時のみ0</returns>
		private static int Initialize() {

			Trace.WriteLine("Initialize!");

			if (DxLib_Init() == -1) { //DXライブラリの初期化
				return -1; //失敗したら-1を返す
			}

			Trace.WriteLine("DxLib_Init.");

			//D3D設定
			SetUseZBuffer3D(TRUE);//Zバッファを使うように
			SetWriteZBuffer3D(TRUE);//Zバッファに書き込むように
			SetDrawScreen(DX_SCREEN_BACK);//バッファリング設定
			SetDrawMode(DX_DRAWMODE_BILINEAR);//バイリニア法

			CHUNITHM.Initialize();//CHUNITHMの初期化

			return 0;
		}

	}
}
