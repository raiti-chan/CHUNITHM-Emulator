using System;
using System.Diagnostics;
using System.Windows.Forms;
using CHUNITHM_Emulator.Chunithm;
using CHUNITHM_Emulator.Config;
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

			Trace.WriteLine("Exit");
			DxLib_End();
		}

		/// <summary>
		/// 一番最初に行われるべき初期化処理
		/// </summary>
		private static void PreInitialize() {

			Trace.Listeners.Clear();
			Trace.Listeners.Add(new LogTraceListerToFile());
			Trace.WriteLine("PreInit!!");

			ChangeWindowMode(MainConfig.Instance.IsFullScreen ? FALSE : TRUE);//ウィンドウモードに変更
			SetMainWindowText(NAME + " Ver-" + VERSION);//ウィンドウ名の取得
			SetAlwaysRunFlag(TRUE);//バックグラウンドでも処理を続けるように
			SetUseASyncLoadFlag(MainConfig.Instance.UseASyncLoad ? TRUE : FALSE);
			// SetWindowIconHandle アイコンの設定
			SetUseFPUPreserveFlag(TRUE);//D3DCREATE_FPU_PRESERVEを使うように
			int resolution = MainConfig.Instance.Resolution / 9;
			SetGraphMode(resolution * 16, resolution * 9, 32);//グラフィックモードの設定(X,Y,ColorBits)(ウィンドウサイズも連動)
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

			Trace.WriteLine("DxLib_Init");

			//D3D設定
			SetUseZBuffer3D(TRUE);//Zバッファを使うように
			SetWriteZBuffer3D(TRUE);//Zバッファに書き込むように
			SetDrawScreen(DX_SCREEN_BACK);//バッファリング設定

			CHUNITHM.Initialize();//CHUNITHMの初期化

			return 0;
		}

	}
}
