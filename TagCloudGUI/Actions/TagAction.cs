using TagCloudContainer;
using TagCloudContainer.TagsWithFont;
using TagCloudGraphicalUserInterface.Interfaces;
using TagCloudGraphicalUserInterface.Settings;

namespace TagCloudGraphicalUserInterface.Actions
{
    public class TagAction : IActionForm
    {
        private readonly IImageSettingsProvider image;
        private readonly IPresetsSettings presetsSettings;
        private readonly ICloudCreateSettings settingsCreator;
        private readonly IAlgorithmSettings algorithmSettings;
        private readonly Palette palette;

        public TagAction(ICloudCreateSettings settingsCreator
            , IPresetsSettings presetsSettings
            , IImageSettingsProvider image
            , IAlgorithmSettings algorithmSettings
            , Palette palette)
        {
            this.image = image;
            this.algorithmSettings = algorithmSettings;
            this.palette = palette;
            this.settingsCreator = settingsCreator;
            this.presetsSettings = presetsSettings;
        }

        string IActionForm.Category => "Нарисовать";

        string IActionForm.Name => "TagCloud";

        string IActionForm.Description => "Нарисовать Облако тегов";

        void IActionForm.Perform()
        {
            CheckForSettings();
            SettingsForm.For(algorithmSettings).ShowDialog();
            settingsCreator.PointFigure.Reset();
            settingsCreator.PointFigure.Config = algorithmSettings.PointConfig;
            var cloud = new TagCloud();
            cloud.CreateTagCloud(settingsCreator, InitialTags(algorithmSettings.ImagesDirectory));
            var size = ImageSizer.GetImageSize(cloud.GetRectangles(), algorithmSettings.StartPoint);
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
            if (presetsSettings.PaletteUse == Switcher.On)
                presetsSettings.Drawer.DrawCloudFromPalette(cloud.GetRectangles(), algorithmSettings.StartPoint, image,
                    palette);
            else
                presetsSettings.Drawer.DrawCloudRandomColor(cloud.GetRectangles(), algorithmSettings.StartPoint, image);
        }

        public IEnumerable<ITag> InitialTags(string filePath)
        {
            var text = presetsSettings.txtReader == Switcher.On
                ? presetsSettings.Reader.TxtRead(filePath)
                : presetsSettings.Reader.DocRead(filePath);
            var parsedText = presetsSettings.Parser.Parse(text);
            var filteredTags = presetsSettings.Filtered == Switcher.On
                ? presetsSettings.Filter.Filter(parsedText, w => w.Length > 3)
                : parsedText;
            var formattedTags = presetsSettings.ToLowerCase == Switcher.On
                ? presetsSettings.Formatter.Normalize(filteredTags, x => x.ToLower())
                : filteredTags;
            var freqTags = presetsSettings.FrequencyCounter.GetWordsFrequency(formattedTags);
            return presetsSettings.FontSizer.GetTagsWithSize(freqTags, algorithmSettings.FontSettings);
        }
    }
}
