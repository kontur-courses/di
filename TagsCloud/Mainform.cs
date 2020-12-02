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
        private readonly Dictionary<string, FontFamily> fontFamilies;
        private readonly Dictionary<string, FontStyle> fontStyles;
        private readonly Dictionary<string, IRectanglesConstellator> constellators;
        private readonly TagscloudHandler tagscloudHandler;
        private readonly ITagscloudDrawer tagscloudDrawer;

        public Mainform( IWordsConverter converter, IRectanglesConstellator[] rectanglesConstellators, 
            TagscloudHandler tagscloudHandler, ITagscloudDrawer drawer)
        {
            wordConverter = converter;
            this.tagscloudHandler = tagscloudHandler;
            tagscloudDrawer = drawer;
            fontFamilies = tagscloudHandler.Settings.FontSettings.FontFamilies.ToDictionary(family => family.Name);
            fontStyles = tagscloudHandler.Settings.FontSettings.FontStyles.ToDictionary(style => style.ToString());
            constellators = rectanglesConstellators.ToDictionary(c => c.Name);
            InitializeComponent();
            FontFamilyChoice.DataSource = tagscloudHandler.Settings.FontSettings.FontFamilies.Select(f => f.Name).ToList();
            FontStyleChoice.DataSource = tagscloudHandler.Settings.FontSettings.FontStyles.Select(f => f.ToString()).ToList();
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
            builder.Register(a => tagscloudHandler.ExcludedWords).AsSelf();
            builder.Register(a => wordConverter).AsSelf();
            builder.RegisterType<SetExcludingWordsForm>().AsSelf();
            var container = builder.Build();
            container.Resolve<SetExcludingWordsForm>().ShowDialog();
        }

        private void FontFamilyChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            tagscloudHandler.Settings.CurrentFontFamily = fontFamilies[(string)FontFamilyChoice.SelectedItem];
        }

        private void FontStyleChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            tagscloudHandler.Settings.CurrentFontStyle = fontStyles[(string) FontStyleChoice.SelectedItem];
        }

        private void AlgorithmChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            tagscloudDrawer.SetNewConstellator(constellators[(string)AlgorithmChoice.SelectedItem]);
        }
    }
}
