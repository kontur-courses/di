using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TagCloud.GUI.Properties;
using TagCloud.Utility;
using TagCloud.Utility.Container;
using TagCloud.Visualizer.Settings;

namespace TagCloud.GUI
{
    public partial class TagCloudForm : Form
    {
        private readonly Options options = Options.Standart;

        public TagCloudForm()
        {
            InitializeComponent();
            SetUp();
        }

        private void SetUp()
        {
            XBox.Text = options.Size.Split('x').First();
            YBox.Text = options.Size.Split('x').Last();
            Format.Items.AddRange(Enum.GetNames(typeof(DrawFormat)));
            Format.SetSelected((int)options.DrawFormat, true);
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
            using (var fileDialog = new SaveFileDialog
            {
                Filter = @"Images|*.jpg;*.bmp;*.gif;*.png",
                Title = Resources.TagCloudForm_SaveImage
            })
            {
                if (fileDialog.ShowDialog() != DialogResult.OK)
                    return;
                options.PathToPicture = fileDialog.FileName;
            }

            CollectInfo();
        }

        private void ChangeFontColorButton_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog
            {
                AllowFullOpen = false,
                ShowHelp = true,
                SolidColorOnly = true,
                FullOpen = false,
                Color = ColorTranslator.FromHtml(options.Brush)
            })
                if (colorDialog.ShowDialog() == DialogResult.OK)
                    options.Brush = ColorTranslator.ToHtml(colorDialog.Color);

            CollectInfo();
        }

        private void Format_SelectedIndexChanged(object sender, EventArgs e)
        {
            options.DrawFormat = (DrawFormat)Format.SelectedIndex;
        }

        private void ChangeFontButton_Click(object sender, EventArgs e)
        {
            var converter = new FontConverter();
            using (var fontDialog = new FontDialog
            {
                ShowHelp = true,
                MaxSize = 1,
                MinSize = 1,
                ShowColor = false,
                ShowEffects = false,
                ShowApply = false,
                Font = converter.ConvertFrom(options.Font) as Font
            })
                if (fontDialog.ShowDialog() == DialogResult.OK)
                    options.Font = converter.ConvertToString(fontDialog.Font);

            CollectInfo();
        }

        private void XBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(XBox.Text, out var res))
                options.Size = $"{res}x{options.Size.Split('x').Last()}";
            CollectInfo();
        }

        private void YBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(YBox.Text, out var res))
                options.Size = $"{options.Size.Split('x').First()}x{res}";
            CollectInfo();
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            Picture.Image?.Dispose();

            if (options.PathToWords == null)
                loadTextFileToolStripMenuItem.PerformClick();
            if (options.PathToWords == null)
                return;

            if (options.PathToPicture == null)
                saveImageToolStripMenuItem.PerformClick();
            if (options.PathToPicture == null)
                return;

            try
            {
                TagCloudProgram.Execute(options);
                Picture.Image = Image.FromFile(options.PathToPicture);
            }
            catch (Exception exception)
            {
                while (exception.InnerException != null)
                    exception = exception.InnerException;
                Output.Text = "ERROR: " + exception.Message;
            }
        }

        private void CollectInfo()
        {
            var sb = new StringBuilder();
            sb.Append(GetLine("Path to text", options.PathToWords));
            sb.Append(GetLine("Path to picture", options.PathToPicture));
            sb.Append(GetLine("Path to stopwords", options.PathToStopWords));
            sb.Append(GetLine("Path to tags", options.PathToTags));
            sb.Append(GetLine("Font color", options.Brush));
            sb.Append(GetLine("background color", options.Color));
            sb.Append(GetLine("Font", options.Font));
            sb.Append(GetLine("Size", options.Size));
            Output.Text = sb.ToString();
        }

        private string GetLine(string name, string value)
        {
            if (value == null)
                return null;
            return $"{name.ToUpper()}:" + Environment.NewLine
                   + value + Environment.NewLine
                   + Environment.NewLine;
        }

        private void SetStopwordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var selectFileDialog = new OpenFileDialog
            {
                Filter = @"Text|*.txt;*.ini",
                Title = Resources.TagCloudForm_AddStopwords
            })
                options.PathToStopWords = selectFileDialog.ShowDialog() == DialogResult.OK
                    ? selectFileDialog.FileName
                    : null;

            CollectInfo();
        }

        private void SetTagsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var selectFileDialog = new OpenFileDialog
            {
                Filter = @"Text|*.txt;*.ini",
                Title = Resources.TagCloudForm_SetTagsFile
            })
                options.PathToTags = selectFileDialog.ShowDialog() == DialogResult.OK
                    ? selectFileDialog.FileName
                    : null;

            CollectInfo();
        }

        private void ChangeBackgroundColorButton_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog
            {
                AllowFullOpen = false,
                ShowHelp = true,
                SolidColorOnly = true,
                FullOpen = false,
                Color = ColorTranslator.FromHtml(options.Color)
            })
                if (colorDialog.ShowDialog() == DialogResult.OK)
                    options.Color = ColorTranslator.ToHtml(colorDialog.Color);

            CollectInfo();
        }
    }
}
