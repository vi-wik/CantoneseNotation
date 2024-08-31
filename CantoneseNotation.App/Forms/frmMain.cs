using CantoneseNotation.Business;
using System.Text;

namespace CantoneseNotation.App
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void Init()
        {
            this.webView.EnsureCoreWebView2Async();
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
            this.webView.CoreWebView2.NavigateToString("处理中...");

            bool useGP = this.rbGP.Checked;
            var lines = this.txtSource.Lines;
            char[] separators = [' ', ',', ';', '.', '，', '；', '。', '、'];

            StringBuilder sbResult = new StringBuilder(@"<meta charset=""UTF-8"">");
            
            this.AppendStyle(sbResult);

            int count = 0;

            foreach (var line in lines)
            {
                count++;

                if (count > 1)
                {
                    this.AppendLine(sbResult);
                    this.AppendLine(sbResult);
                }

                var items = line.Split(separators);

                int i = 0;
                int lineCharsCount = 0;

                foreach (var item in items)
                {
                    if (i > 0)
                    {
                        this.AppendSpace(sbResult);
                    }

                    var syllables = await DataProcessor.GetSyllables(item);

                    for (int j = 0; j < item.Length; j++)
                    {
                        var syllable = syllables.FirstOrDefault(item => item.OriginalWordIndex == j);

                        if (syllable != null)
                        {
                            string pinyin = useGP ? syllable.SyllableFull_GP_Display : syllable.SyllableFull_Display;

                            if (j > 0)
                            {
                                this.AppendSpace(sbResult);
                            }

                            this.AppendWord(sbResult, syllable.Word, pinyin);
                        }
                        else
                        {
                            this.AppendText(sbResult, item[j].ToString());
                        }

                        lineCharsCount++;
                    }

                    if (lineCharsCount < line.Length)
                    {
                        char c = line[lineCharsCount];

                        if (separators.Contains(c))
                        {
                            sbResult.Append(c);
                            lineCharsCount++;
                        }
                    }

                    i++;
                }
            }   

            this.webView.CoreWebView2.NavigateToString(sbResult.ToString());
        }

        private void AppendStyle(StringBuilder sb)
        {
            sb.AppendLine("<style>");

            var lines = File.ReadAllLines("Styles/WebBrowser.css");

            foreach (var line in lines)
            {
                sb.AppendLine(line);
            }

            sb.AppendLine("</style>");
        }

        private void AppendWord(StringBuilder sb, string word, string pinyin)
        {
            sb.Append($"<ruby class='word'>{word}<rt><div><span class='pinyin'>{this.FormatPinyin(pinyin)}</span></div></rt></ruby>");
        }

        private string FormatPinyin(string pinyin)
        {
            if(string.IsNullOrEmpty(pinyin))
            {
                return string.Empty;
            }

            bool useToneUp = this.chkToneUp.Checked;

            if(!useToneUp)
            {
                return pinyin;
            }

            int? tone = default(int?);

            char lastChar = pinyin.LastOrDefault();

            if(int.TryParse(lastChar.ToString(), out _))
            {
                tone = int.Parse(lastChar.ToString());
            }

            if(tone.HasValue)
            {
                return pinyin.Substring(0, pinyin.Length - 1) + $"<sup>{tone}</sup>";
            }

            return pinyin;
        }

        private void AppendText(StringBuilder sb, string text)
        {
            sb.Append($"<span class='text'>{text}</span>");
        }

        private void AppendSpace(StringBuilder sb)
        {
            sb.Append("&nbsp;");
        }

        private void AppendLine(StringBuilder sb)
        {
            sb.Append("<br/>");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.txtSourceFile.Text = this.openFileDialog1.FileName;

                this.txtSource.Text = File.ReadAllText(this.openFileDialog1.FileName);
            }
        }

        private void trbLrc_Click(object sender, EventArgs e)
        {
            frmLrc frmLrc = new frmLrc();

            frmLrc.Show();
        }
    }
}
