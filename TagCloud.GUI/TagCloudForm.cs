using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TagCloud.GUI.Properties;
using TagCloud.Utility;
using TagCloud.Utility.Data;
using TagCloud.Visualizer.Settings;

namespace TagCloud.GUI
{
    public partial class TagCloudForm : Form
    {
        private readonly Options options = new Options
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
            Format.SetSelected(1,true);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var selectFileDialog = new OpenFileDialog
            {
                Filter = @"Text|*.txt;*.ini",
                Title = Resources.TagCloudForm_LoadTextFile
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
                Filter = @"Images|*.jpg;*.bmp;*.gif;*.png",
                Title = Resources.TagCloudForm_SaveImage
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
                MaxSize = 1,
                MinSize = 1,
                ShowColor = false,
                ShowEffects = false,
                ShowApply = false,
                Font = converter.ConvertFrom(options.Font) as Font
            };

            if (fontDialog.ShowDialog() == DialogResult.OK)
                options.Font = converter.ConvertToString(fontDialog.Font);
            CollectInfo();
        }

        private void XBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(XBox.Text, out var res))
                options.Size = $"{res}x{options.Size.Split('x').Last()}";
            else
                XBox.Text = Resources.TagCloudForm_ResolutionStandart;
            CollectInfo();
        }

        private void YBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(YBox.Text, out var res))
                options.Size = $"{options.Size.Split('x').First()}x{res}";
            else
                YBox.Text = Resources.TagCloudForm_ResolutionStandart;
            CollectInfo();
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            if (options.PathToWords == null)
                loadTextFileToolStripMenuItem.PerformClick();
            if (options.PathToPicture == null)
                saveImageToolStripMenuItem.PerformClick();
            if (options.PathToWords == null || options.PathToPicture == null)
                return;

            var logger = new Logger();
            Utility.Program.Start(options, logger);

            if (logger.Exceptions.Any())
                Output.Text = string.Concat(logger.Exceptions.Select(er => er.Message));
            else
            {
                Picture.Image = logger.Picture;
                Output.Text += Resources.TagCloudForm_DrawButton_DONE___ + Environment.NewLine;
            }
        }

        private void CollectInfo()
        {
            var sb = new StringBuilder();
            sb.Append(GetLine("Path to text", options.PathToWords));
            sb.Append(GetLine("Path to picture", options.PathToPicture));
            sb.Append(GetLine("Path to stopwords", options.PathToStopWords));
            sb.Append(GetLine("Path to tags", options.PathToTags));
            sb.Append(GetLine("Color", options.Color));
            sb.Append(GetLine("Font", options.Font));
            sb.Append(GetLine("Size", options.Size));
            Output.Text = sb.ToString();
        }

        private string GetLine(string name, string value)
        {
            if (value == null) return null;
            return $"{name.ToUpper()}:" + Environment.NewLine
                   + value + Environment.NewLine
                   + Environment.NewLine;
        }

        private void AddStopwordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var selectFileDialog = new OpenFileDialog
            {
                Filter = @"Text|*.txt;*.ini",
                Title = Resources.TagCloudForm_AddStopwords
            })
            {
                if (selectFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                options.PathToStopWords = selectFileDialog.FileName;
            }
            CollectInfo();
        }

        private void SetTagsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var selectFileDialog = new OpenFileDialog
            {
                Filter = @"Text|*.txt;*.ini",
                Title = Resources.TagCloudForm_SetTagsFile
            })
            {
                if (selectFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                options.PathToTags = selectFileDialog.FileName;
            }
            CollectInfo();
        }
    }
}
