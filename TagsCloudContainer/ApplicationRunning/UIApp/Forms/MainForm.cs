using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FluentAssertions;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.CloudVisualizers.BitmapMakers;
using TagsCloudContainer.CloudVisualizers.ImageSaving;
using TagsCloudContainer.TextParsing.CloudParsing.ParsingRules;
using TagsCloudContainer.TextParsing.FileWordsParsers;

namespace TagsCloudContainer.ApplicationRunning.UIApp.Forms
{
    public partial class MainForm : Form
    {
        private SettingsManager settings;
        private TagsCloud cloud;

        public MainForm(TagsCloud cloud, SettingsManager settings)
        {
            this.cloud = cloud;
            this.settings = settings;
            InitializeComponent();
            
        }

        private void ChooseTextFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            var filePath = openFileDialog1.FileName;
            var extension = Path.GetExtension(filePath);
            var parser = WordsParser.GetParser(extension);
            settings.ConfigureWordsParserSettings(parser, filePath, new DefaultParsingRule());
            cloud.ParseWords();
            EnableButtons();
            GenerateButton.PerformClick();
        }

        private void EnableButtons()
        {
            GenerateButton.Enabled = true;
            ApplyVisualizationButton.Enabled = true;
            SaveImageButton.Enabled = true;
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            var algorithmName = LayoutingAlgorithmComboBox.SelectedItem.ToString();
            var step = (double)LayouterStep.Value;
            var broadness = (int)LayouterBroadness.Value;
            var size = (int)LayouterSquareMultiplier.Value;
            var algorithm = CloudLayoutingAlgorithms.TryGetLayoutingAlgorithm(algorithmName, step, broadness);
            settings.ConfigureLayouterSettings(algorithm, size, step, broadness);
            cloud.GenerateTagCloud();
            ApplyVisualizationButton.PerformClick();
        }

        private void ApplyVisualizationButton_Click(object sender, EventArgs e)
        {
            var width = (int) WidthNumeric.Value;
            var height = (int) HeightNumeric.Value;
            var background = BackgroundColorDialog.Color;
            var first = FirstColorDialog.Color;
            var second = SecondColorDialog.Color;
            var isGradient = GradientCheckBox.Checked;
            var palette = new Palette
            {
                BackgroundColor = background, IsGradient = isGradient,
                PrimaryColor = first, SecondaryColor = second
            };
            var font = fontDialog1.Font;
            settings.ConfigureVisualizerSettings(palette, BitmapMakers.TryGetBitmapMaker("def"), width, height, font);
            cloud.VisualizeCloud();
            PreviewPictureBox.Image = cloud.VisualizedBitmap;
        }

        private void FontButton_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
        }

        private void BackgroundColorButton_Click(object sender, EventArgs e)
        {
            BackgroundColorDialog.ShowDialog();
        }

        private void FirstColorButton_Click(object sender, EventArgs e)
        {
            FirstColorDialog.ShowDialog();
        }

        private void SecondColorButton_Click(object sender, EventArgs e)
        {
            SecondColorDialog.ShowDialog();
        }

        private void SaveImageDialog_FileOk(object sender, CancelEventArgs e)
        {
            var filePath = SaveImageDialog.FileName;
            var extension = Path.GetExtension(filePath);
            var format = SupportedImageFormats.TryGetSupportedImageFormats(extension);
            settings.ConfigureImageSaverSettings(format, filePath);
            cloud.SaveVisualized();
        }

        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            SaveImageDialog.ShowDialog();
        }
    }
}