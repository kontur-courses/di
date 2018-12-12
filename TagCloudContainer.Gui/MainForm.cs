using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer;
using TagsCloudContainer.ResultRenderer;
using TagsCloudContainer.WordFormatters;
using TagsCloudContainer.WordLayouts;
using TagsCloudContainer.WordsPreprocessors;
using TagsCloudContainer.WordsReaders;

namespace TagCloudContainer.Gui
{
    public partial class MainForm : Form
    {
        private IContainer container;
        private Color chooseColor = Color.BlueViolet;
        private Image resultImage; // диспозится в "partial class MainForm" и при каждой генерации картинки

        public MainForm()
        {
            InitializeComponent();
            container = new CloudContainerBuilder().BuildTagsCloudContainer();
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
                    var reader = container
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
            var font = new Font(FontFamily.GenericMonospace, 12);

            resultImage?.Dispose();

            using (var scope = container.BeginLifetimeScope())
            {
                var config = scope.Resolve<Config>();
                config.Color = chooseColor;
                config.Font = font;
                config.ImageSize = size;

                resultImage = scope.Resolve<TagsCloudBuilder>()
                    .Visualize(wordsTextBox.Lines);
            }

            resultPictureBox.Image = resultImage;
        }
    }
}