using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using TagsCloud.App;
using TagsCloud.Infrastructure;

namespace TagsCloud
{
    public partial class Mainform : Form
    {
        private readonly IWordsConverter wordConverter;
        private readonly Dictionary<string, IRectanglesConstellator> constellators;
        private readonly TagscloudHandler tagscloudHandler;
        private readonly ITagscloudDrawer tagscloudDrawer;

        public Mainform( IWordsConverter converter,
            IRectanglesConstellator[] rectanglesConstellators, TagscloudHandler tagscloudHandler, ITagscloudDrawer drawer)
        {
            wordConverter = converter;
            this.tagscloudHandler = tagscloudHandler;
            tagscloudDrawer = drawer;
            constellators = rectanglesConstellators.ToDictionary(c => c.Name);
            InitializeComponent();
            AlgorithmChoice.DataSource = rectanglesConstellators.Select(c => c.Name).ToList();
        }

        private void SetPaletteButton_Click(object sender, EventArgs e)
        {
            new SettingsForm<Palette>(tagscloudHandler.Settings.Palette).ShowDialog();
        }

        private void SetImageSizeButton_Click(object sender, EventArgs e)
        {
            new SettingsForm<ImageSize>(tagscloudHandler.Settings.ImageSize).ShowDialog();
        }

        private void SetFontButton_Click(object sender, EventArgs e)
        {
            new SettingsForm<Font>(tagscloudHandler.Settings.WordsFont).ShowDialog();
        }

        private void ImageSaveButton_Click(object sender, EventArgs e)
        {
            if (PictureBox.Image != null && SaveFileDialog.ShowDialog() == DialogResult.OK)
                PictureBox.Image.Save(SaveFileDialog.FileName);
        }

        private void TextOpenButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                tagscloudHandler.SetWords(File.ReadAllLines(OpenFileDialog.FileName));
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            PictureBox.Image = tagscloudHandler.GetNewTagcloud();
        }

        private void SetExcludedWordsButton_Click(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.Register(a => tagscloudHandler.excludedWords).AsSelf();
            builder.Register(a => wordConverter).AsSelf();
            builder.RegisterType<SetExcludingWordsForm>().AsSelf();
            var container = builder.Build();
            container.Resolve<SetExcludingWordsForm>().ShowDialog();
        }

        private void AlgorithmChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            tagscloudDrawer.SetNewConstellator(constellators[(string)AlgorithmChoice.SelectedItem]);
        }
    }
}
