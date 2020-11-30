using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TagsCloud.App;
using TagsCloud.Infrastructure;

namespace TagsCloud
{
    public partial class Mainform : Form
    {
        private readonly TagcloudSettings settings;
        private string[] words;
        private string[] excludedWords;
        private IWordsConverter wordConverter;

        public Mainform(TagcloudSettings settings, IWordsConverter converter)
        {
            this.settings = settings;
            wordConverter = converter;
            words = new string[0];
            excludedWords = new[]
            {
                "и",
                "a",
                "в"
            };
            InitializeComponent();
        }

        private void SetPaletteButton_Click(object sender, EventArgs e)
        {
            new SettingsForm<Palette>(settings.Palette).ShowDialog();
        }

        private void SetImageSizeButton_Click(object sender, EventArgs e)
        {
            new SettingsForm<ImageSize>(settings.ImageSize).ShowDialog();
        }

        private void SetFontButton_Click(object sender, EventArgs e)
        {
            new SettingsForm<Font>(settings.WordsFont).ShowDialog();
        }

        private void ImageSaveButton_Click(object sender, EventArgs e)
        {
            if (PictureBox.Image != null && SaveFileDialog.ShowDialog() == DialogResult.OK)
                PictureBox.Image.Save(SaveFileDialog.FileName);
        }

        private void TextOpenButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                words = File.ReadAllLines(OpenFileDialog.FileName);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var neededWords = new WordsConverter().ConvertWords(words)
                .ToList();
            var counts = new WordsCounter().CountWords(neededWords);
            PictureBox.Image = new TagscloudDrawer().GetTagscloud(counts, settings, 0.7d);
        }

        private void SetExcludedWordsButton_Click(object sender, EventArgs e)
        {
            new SetExcludingWordsForm().ShowDialog();
        }
    }
}
