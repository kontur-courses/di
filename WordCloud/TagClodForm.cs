using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WordCloud.CloudControl;
using WordCloud.LayoutGeneration.Layoter;
using WordCloud.TextAnalyze;
using WordCloud.TextAnalyze.Extractors;
using WordCloud.TextAnalyze.Words;
using WordCloud.WordCloudRenedering;

namespace WordCloud
{
    public partial class TagClodForm : Form
    {
        private IBlackList blackList;
        private IWordExtractor wordExtractor;
        private WordCloudOptions wordCloudOptions;

        public TagClodForm(IWordExtractor wordExtractor, IBlackList blackList, WordCloudOptions wordCloudOptions)
        {
            this.wordExtractor = wordExtractor;
            this.blackList = blackList;
            this.wordCloudOptions = wordCloudOptions;
            InitializeComponent();

            ApplyWordCloudOptions();
        }

        private void ApplyWordCloudOptions()
        {
            wordCloudOptions.LayoutType =
                this.spiralLayoutRadioButton.Checked ? LayoutTypes.Circular : LayoutTypes.Orthogonal;

            this.cloudControl.vizualizer = wordCloudOptions.Vizualizer;
            this.cloudControl.MaxFontSize = (int) maxFont.Value;
            this.cloudControl.MinFontSize = (int) minFont.Value;
            this.cloudControl.LayoutType = wordCloudOptions.LayoutType;

        }
       
        private void BuildLayout()
        {
            ApplyWordCloudOptions();
            var text = analyzedText.Text;

            var weightedWords = wordExtractor.GetWords(text)
                .Filter(blackList)
                .CountEntries()
                .SortByEntries();

            this.cloudControl.ArrangeLayout(weightedWords);
            SaveLayout();
        }

        private void SaveLayout()
        {
            if (cloudControl.Image != null)
            {
                var fileName = "cloud.png";
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                cloudControl.Image.Save(fullPath);
                savedImgTxt.Text = fullPath;
                savedImgTxt.Enabled = true;
            }
        }

        private void GoBtn_Click(object sender, EventArgs e)
        {
            BuildLayout();
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            if (openTextFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openTextFileDialog.FileName;
            string fileText = File.ReadAllText(filename);
            analyzedText.Text = fileText;
        }

        private void minFont_ValueChanged(object sender, EventArgs e)
        {
            this.cloudControl.MinFontSize = ((int) minFont.Value);
        }

        private void maxFont_ValueChanged(object sender, EventArgs e)
        {
            this.cloudControl.MaxFontSize = ((int) minFont.Value);
        }

    }
}
