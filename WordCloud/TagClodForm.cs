using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WordCloud.TextAnalyze;
using WordCloud.TextAnalyze.Extractors;
using WordCloud.TextAnalyze.Words;

namespace WordCloud
{
    public partial class TagClodForm : Form
    {
        private IBlackList blackList;
        private IWordExtractor wordExtractor;

        public TagClodForm(IWordExtractor wordExtractor, IBlackList blackList, CloudControl cloudControl)
        {
            this.wordExtractor = wordExtractor;
            this.blackList = blackList;
            this.cloudControl = cloudControl;
            InitializeComponent();
            SetMinMaxFonts();
        }

        private void SetMinMaxFonts()
        {
            this.cloudControl.SetMinFontSize((int) minFont.Value);
            this.cloudControl.SetMaxFontSize((int) maxFont.Value);
        }

        private void GetLayoutImage()
        {
            var text = analyzedText.Text;

            var weightedWords = wordExtractor.GetWords(text)
                .Filter(blackList)
                .CountOccurences()
                .SortByOccurences();

            this.cloudControl.Draw(weightedWords);

            if (cloudControl.Image != null)
            {
                var fileName = "cloud.png";
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                SaveImage(fullPath, cloudControl.Image);
                savedImgTxt.Text = fullPath;
                savedImgTxt.Enabled = true;
            }
        }

        private void GoBtn_Click(object sender, EventArgs e)
        {
            GetLayoutImage();
        }

        private void SaveImage(string path, Image img)
        {
            img.Save(path);
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            if (openTextFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openTextFileDialog.FileName;
            // читаем файл в строку
            string fileText = File.ReadAllText(filename);
            analyzedText.Text = fileText;
        }

        private void minFont_ValueChanged(object sender, EventArgs e)
        {
            this.cloudControl.SetMinFontSize((int) minFont.Value);
        }

        private void maxFont_ValueChanged(object sender, EventArgs e)
        {
            this.cloudControl.SetMaxFontSize((int) minFont.Value);
        }
    }
}
