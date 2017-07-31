
namespace CHUNITHM_Emulator.Chunithm {
	/// <summary>
	/// システムのプロパティ群
	/// </summary>
	internal class SystemProperties {

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

		#endregion

	}
}
