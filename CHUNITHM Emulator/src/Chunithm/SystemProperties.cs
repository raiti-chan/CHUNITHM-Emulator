
using System.Diagnostics;
using CHUNITHM_Emulator.Chunithm.Enums;
using CHUNITHM_Emulator.Renderer.Scene;

namespace CHUNITHM_Emulator.Chunithm {
	/// <summary>
	/// システムのプロパティ群
	/// </summary>
	internal class SystemProperties {

		#region 定数

		/// <summary>
		/// リソースフォルダへのパス
		/// </summary>
		internal const string ResourceLocation = @"Resource\";

		/// <summary>
		/// スキンフォルダへのパス
		/// </summary>
		internal const string SkinLocation = ResourceLocation + @"Skin\";


		#endregion

		#region Property

		#region FPS関係
		/// <summary>
		/// 現在のFPS
		/// </summary>
		internal int FPS { set; get; } = 0;

		/// <summary>
		/// 現在のシステム内Tick
		/// </summary>
		internal int NowSystemTick { set; get; } = 0;

		/// <summary>
		/// 過去のシステム内Tick
		/// </summary>
		internal int OldSystemTick { set; get; } = 0;

		#endregion

		#region 描画関係

		/// <summary>
		/// 縦のピクセル数
		/// </summary>
		internal int Hight { set; get; }

		/// <summary>
		/// 横のピクセル数
		/// </summary>
		internal int Width { set; get; }

		#endregion

		#region システム情報

		/// <summary>
		/// 実行時のCLRバージョン
		/// </summary>
		internal string CLRVersion { get; } = System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion();

		/// <summary>
		/// ビルド時のCLRバージョン
		/// </summary>
		internal string BuildCLRVersion { get; } = System.Reflection.Assembly.GetExecutingAssembly().ImageRuntimeVersion;

		#endregion

		#region ゲーム情報

		/// <summary>
		/// ゲーム内のトータル経過フレーム
		/// </summary>
		internal long Flames = 0;

		/// <summary>
		/// <see cref="GameState"/>の格納フィールド
		/// </summary>
		private GameState _GameState = GameState.Start;
		/// <summary>
		/// 現在のゲームの状態
		/// </summary>
		internal GameState GameState {
			set {
				Trace.WriteLine("ChangeGameState:" + value);
				this._GameState = value;
			}
			get => this._GameState;
		}

		/// <summary>
		/// <see cref="GameScene"/>の格納フィールド
		/// </summary>
		private IScene _GameScene = null;
		/// <summary>
		/// ゲームのシーンオブジェクト
		/// </summary>
		internal IScene GameScene {
			set {
				Trace.WriteLine("Scene Set:" + value.ToString());
				this._GameScene = value;
			}
			get => this._GameScene;
		}

		#endregion
		#endregion

	}
}
