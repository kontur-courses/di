using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ResultProject;
using TagsCloudVisualization;

namespace DesktopClient
{
    public sealed partial class MainForm : Form
    {
        private PictureBox cloudBox;
        private Button setIgnoreFileButton;
        private Button setTextButton;
        private Button fontButton;
        private Button colorButton;
        private NumericUpDown wordCountToStatistic;
        private NumericUpDown density;
        private NumericUpDown minWordLengthToStatistic;

        private CheckBox isLiteraryText;
        private CheckBox randomColor;
        private Color? color;
        
        private string font = "Arial";
        private string? textFilePath;
        private string ignoreWordsFileName;

        private bool changed;

        private readonly InfoTagsCloud info;
        private readonly TagCloud tagCloud;
        

        public MainForm(TagCloud tagCloud)
        {
            info = tagCloud.Info;
            this.tagCloud = tagCloud;
            InitializeComponent();
        }

        private void CloudDragEnter(object sender, DragEventArgs e) 
        {
            e.Effect = DragDropEffects.Move;
        }

        private void CloudDragDrop(object sender, DragEventArgs e) 
        {
            textFilePath = ((string[]) e.Data.GetData(DataFormats.FileDrop, false)).First();
            changed = true;
            Invalidate();
        }

        private void DrawBitmap(Config config)
        {
            tagCloud.GetBitmap(config)
                .ThenAction(x => cloudBox.Image = x)
                .OnFail(x => MessageBox.Show(x, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (textFilePath is null || !changed) return;
            DrawBitmap(GetConfig());
            changed = false;
        }

        private Config GetConfig()
        {
            var config = new Config
            {
                Size = new Size(720, 720),
                Color = randomColor.Checked ? null : color,
                WordCountToStatistic = wordCountToStatistic.Value > 0 ? (uint)wordCountToStatistic.Value : 0,
                Density = density.Value > 0 ? (uint)density.Value : 1,
                MinWordToStatisticLength = (byte)minWordLengthToStatistic.Value,
                Font = font,
                TextFilePath = textFilePath!,
                IgnoreFilePath = ignoreWordsFileName,
                TagCloudResultActions = TagCloudResultActions.None,
                SourceTextInterpretationMode = isLiteraryText.Checked ? SourceTextInterpretationMode.LiteraryText : SourceTextInterpretationMode.OneWordPerLine
            };

            return config;
        }
    }
}