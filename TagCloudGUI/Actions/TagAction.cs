using TagCloudContainer;
using TagCloudContainer.TagsWithFont;
using TagCloudGraphicalUserInterface.Interfaces;
using TagsCloudVisualization;


namespace TagCloudGraphicalUserInterface.Actions
{
    public class TagAction : IActionForm
    {
        private readonly IImageSettingsProvider imageSettingsProvider;

        IPresetsSettings presetsSettings;
        private ICloudCreateSettings settingsCreator;
        private readonly TagCloudSettings settings;
        private readonly Palette palette;

        public TagAction(ICloudCreateSettings settingsCreator
            , IPresetsSettings presetsSettings
            , IImageSettingsProvider imageSettingsProvider
            , TagCloudSettings settings
            , Palette palette)
        {
            this.imageSettingsProvider = imageSettingsProvider;
            this.settings = settings;
            this.palette = palette;
            this.settingsCreator = settingsCreator;
            this.presetsSettings = presetsSettings;
        }

        string IActionForm.Category => "Нарисовать";

        string IActionForm.Name => "TagCloud";

        string IActionForm.Description => "Нарисовать Облако тегов";

        void IActionForm.Perform()
        {
            SettingsForm.For(settings).ShowDialog();
            settingsCreator.PointFigure.Config = settings.PointConfig;
            var cloud = new TagCloud();
            var tags = InitialTags(settings.ImagesDirectory);
            cloud.CreateTagCloud(settingsCreator, tags);
            var size = GetImageSize(cloud.GetRectangles());
            imageSettingsProvider.RecreateImage(new ImageSettings { Height = size.Height, Width = size.Width });
            presetsSettings.Drawer.DrawCloud(cloud.GetRectangles(), settings.StartPoint, imageSettingsProvider, palette);

        }

        public IEnumerable<ITag> InitialTags(string filePath)
        {
            var fileReader = presetsSettings.txtReader == Switcher.On
                ? presetsSettings.Reader.TxtRead(filePath)
                : presetsSettings.Reader.DocRead(filePath);
            var parsedText = presetsSettings.Parser.Parse(fileReader);
            var filteredTags = presetsSettings.Filtered == Switcher.On
                ? presetsSettings.Filter.Filter(parsedText, w => w.Length > 3)
                : parsedText;
            var formattedTags = presetsSettings.Filtered == Switcher.On
                ? presetsSettings.Formatter.Normalize(filteredTags, x => x.ToLower())
                : filteredTags;
            var freqTags = presetsSettings.FrequencyCounter.GetWordsFrequency(formattedTags);
            return presetsSettings.FontSizer.GetTagsWithSize(freqTags, settings.FontSettings);
        }

        public static Size GetImageSize(IEnumerable<TextRectangle> rectangles)
        {
            var maxLocationX = GetMaxPointLocation(rectangles, x => x.rectangle.X);
            var maxLocationY = GetMaxPointLocation(rectangles, y => y.rectangle.Y);
            return new Size(((Math.Abs(maxLocationX.rectangle.Location.X) + maxLocationX.rectangle.Height) * 3),
                ((Math.Abs(maxLocationY.rectangle.Location.Y) + maxLocationY.rectangle.Height) * 3));
        }

        private static TextRectangle GetMaxPointLocation(IEnumerable<TextRectangle> rectangles,
            Func<TextRectangle, int> func)
        {
            return rectangles.MaxBy(func);
        }
    }
}
