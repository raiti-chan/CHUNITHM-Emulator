using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Restarter {
	class RestarterMain {

		[STAThread]
		private static void Main() {
			try {
				Restart();
			} catch (Exception e) {
				MessageBox.Show("再起動に失敗しました。\n\n Error:" + e.Message, "再起動に失敗しました。", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		private static void Restart() {
			//コマンドライン引数の取得
			string[] args = Environment.GetCommandLineArgs();

			if (args.Length < 4) {
				throw new ArgumentException("コマンドライン引数が足りません。");
			}

			int processId;

			try {
				processId = int.Parse(args[1]);
			} catch (Exception e) {
				throw new ArgumentException("プロセスIDが不正です。", e);
			}

			int wateTime;

			try {
				wateTime = int.Parse(args[2]);
			} catch (Exception e) {
				throw new ArgumentException("待機時間が不正です。", e);
			}

			string exePath = args[3];
			if (!File.Exists(exePath)) {
				throw new ArgumentException("指定された実行ファイルが見つかりません。");
			}

			string cmd = "";

			for (int i = 4; i < args.Length; i++) {
				if (i > 4) {
					cmd += " ";
				}

				cmd += "\"" + args[i] + "\"";
			}

			Process process;
			try {
				process = Process.GetProcessById(processId);
			} catch {
				process = null;
			}

			if (process != null) {
				if (!process.WaitForExit(wateTime)) {
					throw new Exception("プロセスが終了しませんでした。");
				}
				process.Close();
			}

			Process.Start(exePath, cmd);

		}
	}
}
