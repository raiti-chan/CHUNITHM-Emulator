namespace CHUNITHM_Emulator.Dialog {
	partial class WindowModeDialog {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.HightL = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.WidthTB = new System.Windows.Forms.MaskedTextBox();
			this.IsFullScreenCB = new System.Windows.Forms.CheckBox();
			this.Ok = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 9);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(381, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "解像度の設定\r\n(フルスクリーンが有効でない場合は解像度がそのままウィンドウサイズになります。)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(94, 37);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "×";
			// 
			// HightL
			// 
			this.HightL.AutoSize = true;
			this.HightL.Location = new System.Drawing.Point(113, 37);
			this.HightL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.HightL.Name = "HightL";
			this.HightL.Size = new System.Drawing.Size(35, 12);
			this.HightL.TabIndex = 3;
			this.HightL.Text = "label3";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(36, 57);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "(16:9固定)";
			// 
			// WidthTB
			// 
			this.WidthTB.Location = new System.Drawing.Point(10, 35);
			this.WidthTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.WidthTB.Mask = "9999";
			this.WidthTB.Name = "WidthTB";
			this.WidthTB.Size = new System.Drawing.Size(83, 19);
			this.WidthTB.TabIndex = 5;
			this.WidthTB.TextChanged += new System.EventHandler(this.Width_TextChanged);
			// 
			// IsFullScreenCB
			// 
			this.IsFullScreenCB.AutoSize = true;
			this.IsFullScreenCB.Location = new System.Drawing.Point(10, 89);
			this.IsFullScreenCB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.IsFullScreenCB.Name = "IsFullScreenCB";
			this.IsFullScreenCB.Size = new System.Drawing.Size(141, 16);
			this.IsFullScreenCB.TabIndex = 6;
			this.IsFullScreenCB.Text = "フルスクリーンモードにする";
			this.IsFullScreenCB.UseVisualStyleBackColor = true;
			// 
			// Ok
			// 
			this.Ok.AutoSize = true;
			this.Ok.Location = new System.Drawing.Point(288, 127);
			this.Ok.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Ok.Name = "Ok";
			this.Ok.Size = new System.Drawing.Size(45, 22);
			this.Ok.TabIndex = 7;
			this.Ok.Text = "決定";
			this.Ok.UseVisualStyleBackColor = true;
			this.Ok.Click += new System.EventHandler(this.Ok_Click);
			// 
			// Cancel
			// 
			this.Cancel.AutoSize = true;
			this.Cancel.Location = new System.Drawing.Point(337, 127);
			this.Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(62, 22);
			this.Cancel.TabIndex = 8;
			this.Cancel.Text = "キャンセル";
			this.Cancel.UseVisualStyleBackColor = true;
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// WindowModeDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 165);
			this.ControlBox = false;
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.Ok);
			this.Controls.Add(this.IsFullScreenCB);
			this.Controls.Add(this.WidthTB);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.HightL);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(416, 204);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(416, 204);
			this.Name = "WindowModeDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ウィンドウの設定";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label HightL;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.MaskedTextBox WidthTB;
		private System.Windows.Forms.CheckBox IsFullScreenCB;
		private System.Windows.Forms.Button Ok;
		private System.Windows.Forms.Button Cancel;
	}
}