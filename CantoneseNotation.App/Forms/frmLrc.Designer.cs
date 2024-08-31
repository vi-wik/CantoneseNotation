
namespace CantoneseNotation.App
{
    partial class frmLrc
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
            components = new System.ComponentModel.Container();
            btnProcess = new Button();
            lblSource = new Label();
            btnOpenFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            label1 = new Label();
            rbYP = new RadioButton();
            rbGP = new RadioButton();
            txtSourceFile = new TextBox();
            nudStartLineNumber = new NumericUpDown();
            label2 = new Label();
            contextMenuStrip2 = new ContextMenuStrip(components);
            tsmiSelectAll = new ToolStripMenuItem();
            tsmiCopy = new ToolStripMenuItem();
            tsmiSave = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            txtResult = new RichTextBox();
            txtSource = new RichTextBox();
            label3 = new Label();
            toolTip1 = new ToolTip(components);
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)nudStartLineNumber).BeginInit();
            contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnProcess
            // 
            btnProcess.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnProcess.Location = new Point(831, 229);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(75, 23);
            btnProcess.TabIndex = 1;
            btnProcess.Text = "处理";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // lblSource
            // 
            lblSource.AutoSize = true;
            lblSource.Location = new Point(8, 15);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(44, 17);
            lblSource.TabIndex = 13;
            lblSource.Text = "来源：";
            // 
            // btnOpenFile
            // 
            btnOpenFile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOpenFile.Location = new Point(856, 12);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(42, 23);
            btnOpenFile.TabIndex = 15;
            btnOpenFile.Text = "浏览";
            btnOpenFile.UseVisualStyleBackColor = true;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "歌词文件|*.lrc|文本文件|*.txt|所有文件|*.*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 231);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 16;
            label1.Text = "拼音方案：";
            // 
            // rbYP
            // 
            rbYP.AutoSize = true;
            rbYP.Checked = true;
            rbYP.Location = new Point(80, 229);
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
            rbGP.Location = new Point(136, 229);
            rbGP.Name = "rbGP";
            rbGP.Size = new Size(50, 21);
            rbGP.TabIndex = 18;
            rbGP.Text = "广拼";
            rbGP.UseVisualStyleBackColor = true;
            // 
            // txtSourceFile
            // 
            txtSourceFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSourceFile.Location = new Point(46, 12);
            txtSourceFile.Name = "txtSourceFile";
            txtSourceFile.Size = new Size(804, 23);
            txtSourceFile.TabIndex = 21;
            // 
            // nudStartLineNumber
            // 
            nudStartLineNumber.Location = new Point(261, 229);
            nudStartLineNumber.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudStartLineNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudStartLineNumber.Name = "nudStartLineNumber";
            nudStartLineNumber.Size = new Size(47, 23);
            nudStartLineNumber.TabIndex = 22;
            nudStartLineNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(228, 232);
            label2.Name = "label2";
            label2.Size = new Size(32, 17);
            label2.TabIndex = 23;
            label2.Text = "从第";
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { tsmiSelectAll, tsmiCopy, tsmiSave });
            contextMenuStrip2.Name = "contextMenuStrip1";
            contextMenuStrip2.Size = new Size(101, 70);
            contextMenuStrip2.Opening += contextMenuStrip2_Opening;
            // 
            // tsmiSelectAll
            // 
            tsmiSelectAll.Name = "tsmiSelectAll";
            tsmiSelectAll.Size = new Size(100, 22);
            tsmiSelectAll.Text = "全选";
            tsmiSelectAll.Click += tsmiSelectAll_Click;
            // 
            // tsmiCopy
            // 
            tsmiCopy.Name = "tsmiCopy";
            tsmiCopy.Size = new Size(100, 22);
            tsmiCopy.Text = "复制";
            tsmiCopy.Click += tsmiCopy_Click;
            // 
            // tsmiSave
            // 
            tsmiSave.Name = "tsmiSave";
            tsmiSave.Size = new Size(100, 22);
            tsmiSave.Text = "保存";
            tsmiSave.Click += tsmiSave_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.Filter = "歌词文件|*.lrc|文本文件|*.txt|所有文件|*.*";
            // 
            // txtResult
            // 
            txtResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtResult.ContextMenuStrip = contextMenuStrip2;
            txtResult.Font = new Font("Microsoft YaHei UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtResult.Location = new Point(4, 256);
            txtResult.Name = "txtResult";
            txtResult.Size = new Size(902, 236);
            txtResult.TabIndex = 25;
            txtResult.Text = "";
            txtResult.KeyDown += txtResult_KeyDown;
            // 
            // txtSource
            // 
            txtSource.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSource.ContextMenuStrip = contextMenuStrip2;
            txtSource.Font = new Font("Microsoft YaHei UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtSource.Location = new Point(4, 41);
            txtSource.Name = "txtSource";
            txtSource.Size = new Size(902, 185);
            txtSource.TabIndex = 26;
            txtSource.Text = "";
            txtSource.KeyDown += txtSource_KeyDown;
            txtSource.MouseDoubleClick += txtSource_MouseDoubleClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(312, 232);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 27;
            label3.Text = "行开始处理";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.tip;
            pictureBox1.Location = new Point(378, 232);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(22, 17);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 28;
            pictureBox1.TabStop = false;
            toolTip1.SetToolTip(pictureBox1, "双击开始行可自动输入");
            // 
            // frmLrc
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(910, 496);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(txtSource);
            Controls.Add(txtResult);
            Controls.Add(label2);
            Controls.Add(nudStartLineNumber);
            Controls.Add(txtSourceFile);
            Controls.Add(rbGP);
            Controls.Add(rbYP);
            Controls.Add(label1);
            Controls.Add(btnOpenFile);
            Controls.Add(lblSource);
            Controls.Add(btnProcess);
            Name = "frmLrc";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lrc注音";
            Load += frmLrc_Load;
            ((System.ComponentModel.ISupportInitialize)nudStartLineNumber).EndInit();
            contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnProcess;
        private Label lblSource;       
        private Button btnOpenFile;
        private OpenFileDialog openFileDialog1;
        private Label label1;
        private RadioButton rbYP;
        private RadioButton rbGP;
        private TextBox txtSourceFile;
        private NumericUpDown nudStartLineNumber;
        private Label label2;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem tsmiSave;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem tsmiCopy;
        private ToolStripMenuItem tsmiSelectAll;
        private RichTextBox txtResult;
        private RichTextBox txtSource;
        private Label label3;
        private ToolTip toolTip1;
        private PictureBox pictureBox1;
    }
}
