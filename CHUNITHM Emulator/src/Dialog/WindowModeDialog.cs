using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHUNITHM_Emulator.Dialog {
	/// <summary>
	/// 画面設定をするダイアログ
	/// </summary>
	internal partial class WindowModeDialog : Form {

		/// <summary>
		/// フルスクリーンモードが憂有効か
		/// </summary>
		internal bool IsFullScreen { private set; get; } = false;

		/// <summary>
		/// 設定された幅。
		/// </summary>
		internal int ResolutionWidth { private set; get; } = 0;
		

		/// <summary>
		/// デフォルトの状態でダイアログを生成します。
		/// キャンセルボタンは使用不可になります。
		/// 初回起動時の初期設定に使用します。
		/// </summary>
		public WindowModeDialog() {
			InitializeComponent();
			this.WidthTB.Text = "1920";
			this.HightL.Text = "1080";
			this.IsFullScreenCB.CheckState = CheckState.Checked;
			this.Cancel.Enabled = false;
		}

		/// <summary>
		/// 現在の値を反映してダイアログを生成します。
		/// </summary>
		/// <param name="width">幅</param>
		/// <param name="hight">高さ</param>
		/// <param name="isFullScreen">フルスクリーンモード</param>
		public WindowModeDialog(int width, int hight, bool isFullScreen) {
			InitializeComponent();
			this.WidthTB.Text = width.ToString();
			this.HightL.Text = hight.ToString();
			this.IsFullScreenCB.Checked = isFullScreen;
		}

		private void Width_TextChanged(object sender, EventArgs e) {
			try {
				int width = Convert.ToInt32(this.WidthTB.Text);
				this.HightL.Text = (width / 16 * 9).ToString();
			} catch (Exception) {
				this.HightL.Text = "N/a";
			}
		}

		private void Ok_Click(object sender, EventArgs e) {
			this.IsFullScreen = this.IsFullScreenCB.Checked;
			this.ResolutionWidth = Convert.ToInt32(this.WidthTB.Text);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void Cancel_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
