using MystemHandler;
using TagCloudContainer;
using TagCloudContainer.Interfaces;
using TagCloudGUI.Interfaces;
using TagCloudGUI.Settings;

namespace TagCloudGUI.Actions
{
    public class TagAction : IActionForm
    {
        private readonly IImageSettingsProvider image;
        private readonly IPresetsSettings presetsSettings;
        private readonly ICloudCreateSettings settingsCreator;
        private readonly IAlgorithmSettings algorithmSettings;
        private readonly Palette palette;

        public TagAction(ICloudCreateSettings settingsCreator,
            IPresetsSettings presetsSettings,
            IImageSettingsProvider image,
            IAlgorithmSettings algorithmSettings,
            Palette palette)
        {
            this.image = image;
            this.algorithmSettings = algorithmSettings;
            this.palette = palette;
            this.settingsCreator = settingsCreator;
            this.presetsSettings = presetsSettings;
        }

        string IActionForm.Category => "Рисование";

        string IActionForm.Name => "Нарисовать";

        string IActionForm.Description => "Нарисовать облако тегов";

        void IActionForm.Perform()
        {
            CheckForSettings();
            SettingsForm.For(algorithmSettings).ShowDialog();
            settingsCreator.PointFigure.Reset();

            var cloud = new TagCloud();
            cloud.CreateTagCloud(settingsCreator, InitialTags(algorithmSettings.ImagesDirectory));

            var size = ImageSizer.GetImageSize(cloud.GetRectangles());
            image.RecreateImage(new ImageSettings { Height = size.Height, Width = size.Width });
            
            DrawCloud(cloud);
        }

        private void CheckForSettings()
        {
            if (algorithmSettings.ImagesDirectory is null)
                new SourceTagsAction(algorithmSettings).Perform();

            if (algorithmSettings.Font is null)
                new FontAction(algorithmSettings).Perform();
        }
        private void DrawCloud(TagCloud cloud)
        {
            presetsSettings.Drawer.DrawCloudFromPalette(cloud.GetRectangles(), image,
                    palette);
        }

        public IEnumerable<ITag> InitialTags(string filePath)
        {
            var text = presetsSettings.TxtReader == Switcher.Enabled
                ? presetsSettings.Reader.TxtRead(filePath)
                : presetsSettings.Reader.DocRead(filePath);

            var parsedText = presetsSettings.Parser.Parse(text, presetsSettings.Filtered == Switcher.Enabled);

            var formattedTags = presetsSettings.ToLowerCase == Switcher.Enabled
                ? presetsSettings.Formatter.Normalize(parsedText, x => x.ToLower())
                : parsedText;

            var freqTags = presetsSettings.FrequencyCounter.GetTagsFrequency(formattedTags);

            return presetsSettings.FontSizer.GetTagsWithSize(freqTags, algorithmSettings.FontSettings);
        }
    }
}
