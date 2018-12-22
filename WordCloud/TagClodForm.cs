using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WordCloudImageGenerator;
using WordCloudImageGenerator.LayoutCraetion.Layouters;
using WordCloudImageGenerator.Layouting.Layouters;
using WordCloudImageGenerator.Parsing.BlackList;
using WordCloudImageGenerator.Parsing.Extractors;
using WordCloudImageGenerator.Parsing.Word;

namespace WordCloud
{
    public partial class TagClodForm : Form
    {
        private TagCloud tagCloud;
        private readonly IBlackList blackList;
        private readonly IWordExtractor wordExtractor;
        private readonly ITagCloudVizualizer vizualizer;
        private readonly WordCloudConfig wordCloudConfig;

        public TagClodForm(IWordExtractor wordExtractor, IBlackList blackList, ITagCloudVizualizer vizualizer, WordCloudConfig wordCloudConfig)
        {
            this.wordExtractor = wordExtractor;
            this.blackList = blackList;
            this.vizualizer = vizualizer;
            InitializeComponent();
            this.wordCloudConfig = wordCloudConfig;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void BuildLayout()
        {
            CollectTagCloudOptions();
            var text = analyzedText.Text;

            var weightedWords = wordExtractor.GetWords(text)
                .Filter(blackList)
                .CountEntries()
                .SortByEntries();

            this.tagCloud = new TagCloud(wordCloudConfig, vizualizer);
            pictureBox1.Image = null;
            var imagePath = tagCloud.ArrangeLayout(weightedWords);
            savedImgTxt.Text = imagePath;
            SetImageToPictureBox(imagePath);
        }

        private void CollectTagCloudOptions()
        {
            wordCloudConfig.LayoutType = orthogonalLayoutRadioButton.Checked ? LayoutTypes.Linear: LayoutTypes.Circular;
            wordCloudConfig.MinFontSize = (int) minFont.Value;
            wordCloudConfig.MaxFontSize = (int)maxFont.Value;
        }

        private void SetImageToPictureBox(string imagePath)
        {
            FileStream fileStream = new FileStream(imagePath, FileMode.Open);
            using (fileStream)
                pictureBox1.Image = Image.FromStream(fileStream);
        }

        private void GoBtn_Click(object sender, EventArgs e)
        {
            BuildLayout();
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            if (openTextFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            var filename = openTextFileDialog.FileName;
            var fileText = File.ReadAllText(filename);
            analyzedText.Text = fileText;
        }
    }
}
