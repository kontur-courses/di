using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer;
using TagsCloudContainer.WordLayouts;
using TagsCloudContainer.WordsReaders;

namespace TagCloudContainer.Gui
{
    public partial class MainForm : Form
    {
        private ILifetimeScope scope;
        private Color chooseColor = Color.BlueViolet;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            resultPictureBox.BorderStyle = BorderStyle.FixedSingle;
            resultPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var reader = new CloudContainerBuilder()
                        .BuildReaderContainer()
                        .Resolve<IWordsReader>();
                    wordsTextBox.Text = string.Join(Environment.NewLine,
                        reader.GetWords(openFileDialog.FileName));
                }
            }
        }

        private void SaveResultButton_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog {Filter = "Images (*.png)|*.png"})
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    resultPictureBox.Image.Save(saveFileDialog.FileName, ImageFormat.Png);
                }
            }
        }

        private void ChooseFontColor_Click(object sender, EventArgs e)
        {
            var colorPickerDialog = new ColorDialog {Color = chooseColor};
            colorPickerDialog.ShowDialog();
            chooseColor = colorPickerDialog.Color;
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            var size = new Size(int.Parse(resultWidthTextBox.Text), int.Parse(resultHeightTextBox.Text));
            var config = new Config(size, new Font(FontFamily.GenericMonospace, 12), chooseColor);
            var layoutConfig = new CircularCloudLayoutConfig(PointF.Empty, 10);

            scope = new CloudContainerBuilder()
                .BuildTagsCloudContainer(config, layoutConfig)
                .BeginLifetimeScope();

            var tagCloud = scope.Resolve<TagsCloudBuilder>();
            resultPictureBox.Image = tagCloud.Visualize(wordsTextBox.Lines);
        }
    }
}