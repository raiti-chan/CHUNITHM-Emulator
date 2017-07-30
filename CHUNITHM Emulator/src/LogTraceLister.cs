using System;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace CHUNITHM_Emulator {
	/// <summary>
	/// ファイル用のトレースリスナ
	/// </summary>
	internal class LogTraceListerToFile : DefaultTraceListener {

		/// <summary>
		/// ログファイルのディレクトリのパス
		/// </summary>
		private const string _dirPath = @"log\";

		/// <summary>
		/// トレースリスナを生成します。
		/// </summary>
		internal LogTraceListerToFile() {
			Directory.CreateDirectory(_dirPath);
			LogFileName = _dirPath + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt"; ;

		}

		/// <summary>
		/// インデントを書き込みます
		/// </summary>
		protected override void WriteIndent() {
			NeedIndent = false;

			StringBuilder sb = new StringBuilder(32);

			// 現在時刻をログに記録する
			sb.Append("[");
			sb.Append(DateTime.Now.ToString("HH:mm:ss"));
			sb.Append("] ");

			// インデントの前に時刻およびカウンタを出力する
			Write(sb.ToString());

			// インデントを出力する
			base.WriteIndent();
		}
	}
}