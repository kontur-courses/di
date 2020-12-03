using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.App;
using TagsCloud.Infrastructure;

namespace TagsCloud.UI
{
    public partial class Mainform : Form
    {
        private readonly Dictionary<string, FontFamily> fontFamilies;
        private readonly Dictionary<string, FontStyle> fontStyles;
        private readonly Dictionary<string, IRectanglesLayouter> constellators;
        private readonly TagsCloudHandler tagsCloudHandler;
        private readonly ITagsCloudDrawer tagsCloudDrawer;
        private readonly TagsCloudSettings settings;

        public Mainform(IRectanglesLayouter[] rectanglesConstellators, 
            TagsCloudHandler tagsCloudHandler, ITagsCloudDrawer drawer, TagsCloudSettings settings)
        {
            this.tagsCloudHandler = tagsCloudHandler;
            this.settings = settings;
            tagsCloudDrawer = drawer;
            fontFamilies = settings.FontSettings.FontFamilies.ToDictionary(family => family.Name);
            fontStyles = settings.FontSettings.FontStyles.ToDictionary(style => style.ToString());
            constellators = rectanglesConstellators.ToDictionary(c => c.Name);
            InitializeComponent();
            FontFamilyChoice.DataSource = settings.FontSettings.FontFamilies.Select(f => f.Name).ToList();
            FontStyleChoice.DataSource = settings.FontSettings.FontStyles.Select(f => f.ToString()).ToList();
            AlgorithmChoice.DataSource = rectanglesConstellators.Select(c => c.Name).ToList();
        }

        private void SetPaletteButton_Click(object sender, EventArgs e)
        {
            new SettingsForm<Palette>(settings.Palette).ShowDialog();
        }

        private void SetImageSizeButton_Click(object sender, EventArgs e)
        {
            new SettingsForm<ImageSize>(settings.ImageSize).ShowDialog();
        }

        private void ImageSaveButton_Click(object sender, EventArgs e)
        {
            if (PictureBox.Image != null && SaveFileDialog.ShowDialog() == DialogResult.OK)
                PictureBox.Image.Save(SaveFileDialog.FileName);
        }

        private void TextOpenButton_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                tagsCloudHandler.SetWords(File.ReadAllLines(OpenFileDialog.FileName));
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            PictureBox.Image = tagsCloudHandler.GetNewTagcloud();
        }

        private void FontFamilyChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.CurrentFontFamily = fontFamilies[(string)FontFamilyChoice.SelectedItem];
        }

        private void FontStyleChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.CurrentFontStyle = fontStyles[(string) FontStyleChoice.SelectedItem];
        }

        private void AlgorithmChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            tagsCloudDrawer.SetNewLayouter(constellators[(string)AlgorithmChoice.SelectedItem]);
        }
    }
}
