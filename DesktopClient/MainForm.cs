using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouters;

namespace DesktopClient
{
    public sealed partial class MainForm : Form
    {
        private PictureBox cloudBox;
        private Button setIgnoreFileButton;
        private Button fontButton;
        private Button colorButton;
        private NumericUpDown wordCountToStatistic;
        private NumericUpDown density;
        private NumericUpDown minWordLengthToStatistic;

        private CheckBox isLiteraryText;
        private CheckBox randomColor;
        private Color? color;
        
        private string font = "Arials";
        private string? textFilePath;
        private string ignoreWordsFileName;

        private bool changed;
        
        private readonly TagCloud tagCloud = new();
        

        public MainForm()
        {
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

        protected override void OnPaint(PaintEventArgs e)
        {
            if (textFilePath is null || !changed) return;
            cloudBox.Image = tagCloud.GetBitmap(GetConfig());
            changed = false;
        }

        private Config GetConfig()
        {
            var config = new Config
            {
                Size = new Size(700, 700),
                Color = randomColor.Checked ? null : color,
                WordCountToStatistic = wordCountToStatistic.Value > 0 ? (int)wordCountToStatistic.Value : -1,
                Density = density.Value > 0 ? (double)density.Value : 1d,
                MinWordToStatisticLength = (byte)minWordLengthToStatistic.Value,
                Font = font,
                TextFilePath = textFilePath!,
                CustomIgnoreFilePath = ignoreWordsFileName,
                TagCloudResultActions = TagCloudResultActions.None,
                SourceTextInterpretationMode = isLiteraryText.Checked ? SourceTextInterpretationMode.LiteraryText : SourceTextInterpretationMode.OneWordPerLine
            };

            return config;
        }
    }
}