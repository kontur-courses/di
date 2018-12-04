using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Utility;
using TagCloud.Utility.Data;
using TagCloud.Visualizer.Settings;

namespace TagCloud.GUI
{
    public partial class TagCloudForm : Form
    {
        private Options options = new Options
        {
            Color = "#000000",
            DrawFormat = DrawFormat.WordsInRectangles,
            Font = "arial",
            PathToPicture = null,
            PathToStopWords = null,
            PathToTags = null,
            PathToWords = null,
            Size = "100x100"
        };

        public TagCloudForm()
        {
            InitializeComponent();
            CollectInfo();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var selectFileDialog = new OpenFileDialog
            {
                Filter = "Text|*.txt",
                Title = "Open Text File"
            })
            {
                if (selectFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                options.PathToWords = selectFileDialog.FileName;
            }

            CollectInfo();
        }

        private void SaveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileDialog = new SaveFileDialog
            {
                Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif",
                Title = "Save an Image File"
            };
            if (fileDialog.ShowDialog() != DialogResult.OK)
                return;
            options.PathToPicture = fileDialog.FileName;
            CollectInfo();
        }

        private void ChangeColorButton_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog
            {
                AllowFullOpen = false,
                ShowHelp = true,
                SolidColorOnly = true,
                FullOpen = false,
                Color = ColorTranslator.FromHtml(options.Color)
            };

            if (colorDialog.ShowDialog() == DialogResult.OK)
                options.Color = ColorTranslator.ToHtml(colorDialog.Color);
            CollectInfo();
        }

        private void Format_SelectedIndexChanged(object sender, EventArgs e)
        {
            options.DrawFormat = (DrawFormat)Format.SelectedIndex;
        }

        private void ChangeFontButton_Click(object sender, EventArgs e)
        {
            var converter = new FontConverter();
            var fontDialog = new FontDialog
            {
                ShowHelp = true,
                MaxSize = 15,
                MinSize = 15,
                ShowColor = false,
                ShowEffects = false,
                ShowApply = false,
                Font = converter.ConvertFrom(options.Font) as Font
            };

            if (fontDialog.ShowDialog() == DialogResult.OK)
                options.Font = converter.ConvertToString(fontDialog.Font);
            CollectInfo();
        }

        private void AddStopwordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var selectFileDialog = new OpenFileDialog
            {
                Filter = "Text|*.txt",
                Title = "Open Text File"
            })
            {
                if (selectFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                options.PathToStopWords = selectFileDialog.FileName;
            }
            CollectInfo();
        }

        private void XBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(XBox.Text, out var res))
                options.Size = $"{res}x{options.Size.Split('x').Last()}";
            else
                XBox.Text = "100";
        }

        private void YBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(YBox.Text, out var res))
                options.Size = $"{options.Size.Split('x').First()}x{res}";
            else
                YBox.Text = "100";
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            var log = new Logger();
            if (options.PathToWords == null)
                loadTextFileToolStripMenuItem.PerformClick();
            if (options.PathToPicture == null)
                saveImageToolStripMenuItem.PerformClick();
            Utility.Program.Start(options, log);
            if (log.Exceptions.Any())
                OutputLabel.Text = string.Concat(log.Exceptions.Select(er => er.Message));
            else
            {
                Picture.Image = log.Picture;
                CollectInfo();
                Output.Text += "DONE..." + Environment.NewLine;
            }
        }

        private void CollectInfo()
        {
            Output.Text = "Path to text:" + Environment.NewLine + options.PathToWords + Environment.NewLine +
                          "Path to picture:" + Environment.NewLine + options.PathToPicture + Environment.NewLine +
                          "Color:" + Environment.NewLine + options.Color + Environment.NewLine +
                          "Font:" + Environment.NewLine + options.Font + Environment.NewLine;
            if (options.PathToStopWords != null)
                Output.Text += "Path to stopwords:" + Environment.NewLine +
                               options.PathToStopWords + Environment.NewLine;
            if (options.PathToTags != null)
                Output.Text += "Path to tags:" + Environment.NewLine +
                               options.PathToTags + Environment.NewLine;
        }
    }
}
