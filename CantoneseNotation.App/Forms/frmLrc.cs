using CantoneseNotation.Business;
using CantoneseNotation.Business.Model;
using CantoneseNotation.Utility;
using System;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace CantoneseNotation.App
{
    public partial class frmLrc : Form
    {
        public frmLrc()
        {
            InitializeComponent();
        }

        private void frmLrc_Load(object sender, EventArgs e)
        {

        }


        private async void btnProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtSource.Text.Trim()))
            {
                MessageBox.Show("源文本不能为空！");
            }

            this.Process();
        }

        private async void Process()
        {
            this.txtResult.Text = "处理中...";

            int startLineNumber = (int)this.nudStartLineNumber.Value;
            var lines = this.txtSource.Lines;
            char[] separators = [' ', ',', ';', '.', '，', '；', '。', '、'];

            StringBuilder sbResult = new StringBuilder();

            int count = 0;
            string lastLineTimePart = null;

            foreach (string line in lines)
            {
                string timePart = this.GetLineTimePart(line);                

                if (timePart != null && timePart == lastLineTimePart)
                {
                    lastLineTimePart = timePart;
                    continue;
                }

                lastLineTimePart = timePart;

                sbResult.AppendLine(line);

                count++;

                if (count < startLineNumber)
                {
                    continue;
                }               

                if (timePart != null)
                {
                    string timeContent = timePart.Trim('[',']');

                    string clearPart = timeContent.Replace(":", "").Replace(".", "").Trim();

                    if (BigInteger.TryParse(clearPart, out _))
                    {
                        string content = line.Substring(line.LastIndexOf(']') + 1);

                        var items = content.Split(' ');

                        StringBuilder sbContent = new StringBuilder();

                        int i = 0;

                        foreach (var item in items)
                        {
                            string parseItem = item;

                            var syllables = await DataProcessor.GetSyllables(parseItem);

                            if (i > 0)
                            {
                                sbContent.Append(" ");
                            }

                            int syllableCount = syllables.Count();
                            bool asValid = false;

                            string result = "";
                            int chineseCharCount = GetChineseCharCount(parseItem);

                            if (syllableCount == chineseCharCount && chineseCharCount > 0)
                            {
                                asValid = true;

                                StringBuilder sbItem = new StringBuilder();
                                bool lastIsChineseChar = false;
                                char? lastChar = default(char);

                                for (int j = 0; j < parseItem.Length; j++)
                                {
                                    char c = parseItem[j];

                                    bool isChineseChar = StringHelper.IsChineseChar(c);

                                    if (isChineseChar)
                                    {
                                        bool appendedSpace = false;

                                        if (lastChar.HasValue && IsLetterOrNumber(lastChar.Value) && sbItem.Length > 0)
                                        {
                                            sbItem.Append(' ');

                                            appendedSpace = true;
                                        }

                                        var syllable = syllables.FirstOrDefault(item => item.OriginalWordIndex == j);

                                        if (syllable != null)
                                        {
                                            if (sbItem.Length > 0 && appendedSpace == false)
                                            {
                                                sbItem.Append(' ');
                                            }

                                            sbItem.Append(this.GetSyllablePinyin(syllable));
                                        }

                                        lastIsChineseChar = true;
                                    }
                                    else
                                    {
                                        if (lastIsChineseChar)
                                        {
                                            sbItem.Append(' ');
                                        }

                                        sbItem.Append(c);

                                        lastIsChineseChar = false;
                                    }

                                    lastChar = c;
                                }

                                result = sbItem.ToString().Trim();
                            }

                            if (parseItem.Length == 1 && syllableCount > 1)
                            {
                                asValid = true;
                                var syllable = syllables.First();

                                result = this.GetSyllablePinyin(syllable);
                            }

                            if (syllableCount == parseItem.Length || asValid)
                            {
                                sbContent.Append(result);
                            }
                            else
                            {
                                sbContent.Append(parseItem);
                            }

                            i++;
                        }

                        string newLine = timePart + sbContent;

                        if (string.Compare(line, newLine) != 0)
                        {
                            sbResult.AppendLine(newLine.Trim());
                        }
                    }
                }
            }

            this.txtResult.Text = sbResult.ToString();
        }

        private string GetLineTimePart(string line)
        {
            int index = line.LastIndexOf(']');

            if (index > 0)
            {
                int index1 = line.IndexOf('[');
                int index2 = line.IndexOf("]");

                string timePart = line.Substring(index1, index + 1);

                return timePart;
            }

            return null;
        }

        private string GetSyllablePinyin(SyllableDisplay syllable)
        {
            bool useGP = this.rbGP.Checked;

            return useGP ? syllable.SyllableFull_GP_Display : syllable.SyllableFull_Display;
        }

        private static int GetChineseCharCount(string content)
        {
            return content.Count(item => StringHelper.IsChineseChar(item));
        }

        private static bool IsLetterOrNumber(char c)
        {
            if (char.IsNumber(c) || char.IsLetter(c))
            {
                return true;
            }

            return false;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.txtSourceFile.Text = this.openFileDialog1.FileName;

                this.txtSource.Text = File.ReadAllText(this.openFileDialog1.FileName);
                this.nudStartLineNumber.Value = 1;
                this.txtResult.Text = "";
            }
        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.tsmiCopy.Visible = this.txtResult.SelectedText?.Length > 0;
            this.tsmiSelectAll.Visible = this.tsmiSave.Visible = this.txtResult.Text.Length > 0;
        }

        private void tsmiSelectAll_Click(object sender, EventArgs e)
        {
            this.txtResult.SelectionStart = 0;
            this.txtResult.SelectionLength = this.txtResult.Text.Length;
            this.txtResult.Select();
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            this.SaveResult();
        }

        private void SaveResult()
        {
            DialogResult result = this.saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                var lines = this.txtResult.Lines;

                string content = string.Join(Environment.NewLine, lines.Select(item => item.Trim()));

                File.WriteAllText(this.saveFileDialog1.FileName, content, Encoding.UTF8);

                MessageBox.Show("保存成功");
            }
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            this.txtResult.Copy();
        }

        private void txtResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (this.txtResult.Text.Length == 0)
                {
                    return;
                }

                this.SaveResult();
            }
        }

        private void txtSource_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.txtSource.SelectionStart >= 0)
            {
                int lineIndex = this.txtSource.GetLineFromCharIndex(this.txtSource.SelectionStart);

                this.nudStartLineNumber.Value = lineIndex + 1;
            }
        }

        private void txtSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (this.txtSource.Text.Length == 0 || this.txtSource.SelectionLength == this.txtSource.Text.Length)
                {
                    this.nudStartLineNumber.Value = 1;
                }
            }
        }
    }
}
