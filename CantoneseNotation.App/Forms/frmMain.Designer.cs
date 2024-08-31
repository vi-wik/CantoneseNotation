namespace CantoneseNotation.App
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnProcess = new Button();
            toolStrip1 = new ToolStrip();
            trbLrc = new ToolStripButton();
            txtSource = new TextBox();
            lblSource = new Label();
            txtSourceFile = new TextBox();
            btnOpenFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            label1 = new Label();
            rbYP = new RadioButton();
            rbGP = new RadioButton();
            chkToneUp = new CheckBox();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // btnProcess
            // 
            btnProcess.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnProcess.Location = new Point(831, 250);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(75, 23);
            btnProcess.TabIndex = 1;
            btnProcess.Text = "处理";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { trbLrc });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(910, 36);
            toolStrip1.TabIndex = 11;
            toolStrip1.Text = "toolStrip1";
            // 
            // trbLrc
            // 
            trbLrc.AutoSize = false;
            trbLrc.DisplayStyle = ToolStripItemDisplayStyle.Image;
            trbLrc.Image = Properties.Resources.lrc;
            trbLrc.ImageScaling = ToolStripItemImageScaling.None;
            trbLrc.ImageTransparentColor = Color.Magenta;
            trbLrc.Margin = new Padding(0);
            trbLrc.Name = "trbLrc";
            trbLrc.Size = new Size(36, 36);
            trbLrc.Text = "Lrc注音";
            trbLrc.Click += trbLrc_Click;
            // 
            // txtSource
            // 
            txtSource.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSource.Location = new Point(4, 62);
            txtSource.Multiline = true;
            txtSource.Name = "txtSource";
            txtSource.ScrollBars = ScrollBars.Vertical;
            txtSource.Size = new Size(902, 182);
            txtSource.TabIndex = 12;
            // 
            // lblSource
            // 
            lblSource.AutoSize = true;
            lblSource.Location = new Point(8, 39);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(44, 17);
            lblSource.TabIndex = 13;
            lblSource.Text = "来源：";
            // 
            // txtSourceFile
            // 
            txtSourceFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSourceFile.Location = new Point(58, 36);
            txtSourceFile.Name = "txtSourceFile";
            txtSourceFile.Size = new Size(792, 23);
            txtSourceFile.TabIndex = 14;
            // 
            // btnOpenFile
            // 
            btnOpenFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOpenFile.Location = new Point(856, 36);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(42, 23);
            btnOpenFile.TabIndex = 15;
            btnOpenFile.Text = "浏览";
            btnOpenFile.UseVisualStyleBackColor = true;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "文本文件|*.txt|所有文件|*.*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 256);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 16;
            label1.Text = "拼音方案：";
            // 
            // rbYP
            // 
            rbYP.AutoSize = true;
            rbYP.Checked = true;
            rbYP.Location = new Point(78, 254);
            rbYP.Name = "rbYP";
            rbYP.Size = new Size(50, 21);
            rbYP.TabIndex = 17;
            rbYP.TabStop = true;
            rbYP.Text = "粤拼";
            rbYP.UseVisualStyleBackColor = true;
            // 
            // rbGP
            // 
            rbGP.AutoSize = true;
            rbGP.Location = new Point(134, 254);
            rbGP.Name = "rbGP";
            rbGP.Size = new Size(50, 21);
            rbGP.TabIndex = 18;
            rbGP.Text = "广拼";
            rbGP.UseVisualStyleBackColor = true;
            // 
            // chkToneUp
            // 
            chkToneUp.AutoSize = true;
            chkToneUp.Location = new Point(197, 256);
            chkToneUp.Name = "chkToneUp";
            chkToneUp.Size = new Size(75, 21);
            chkToneUp.TabIndex = 21;
            chkToneUp.Text = "声调置上";
            chkToneUp.UseVisualStyleBackColor = true;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Location = new Point(0, 279);
            webView.Name = "webView";
            webView.Size = new Size(906, 215);
            webView.TabIndex = 22;
            webView.ZoomFactor = 1D;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(910, 496);
            Controls.Add(webView);
            Controls.Add(chkToneUp);
            Controls.Add(rbGP);
            Controls.Add(rbYP);
            Controls.Add(label1);
            Controls.Add(btnOpenFile);
            Controls.Add(txtSourceFile);
            Controls.Add(lblSource);
            Controls.Add(txtSource);
            Controls.Add(toolStrip1);
            Controls.Add(btnProcess);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "粤语注音";
            WindowState = FormWindowState.Maximized;
            Load += frmMain_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnProcess;
        private ToolStrip toolStrip1;
        private ToolStripButton trbLrc;
        private TextBox txtSource;
        private Label lblSource;
        private TextBox txtSourceFile;
        private Button btnOpenFile;
        private OpenFileDialog openFileDialog1;
        private Label label1;
        private RadioButton rbYP;
        private RadioButton rbGP;      
        private CheckBox chkToneUp;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
    }
}
