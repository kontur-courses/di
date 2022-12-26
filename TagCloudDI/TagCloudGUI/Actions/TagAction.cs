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
        private readonly IPointProvider pointFigure;
        private readonly IRectangleBuilder rectangleBuilder;
        private readonly IAlgorithmSettings algorithmSettings;
        private readonly IBoringWordsFilter boringWordsFilter;
        private readonly Palette palette;

        public TagAction(
            IPointProvider pointFigure,
            IRectangleBuilder rectangleBuilder,
            IPresetsSettings presetsSettings,
            IImageSettingsProvider image,
            IAlgorithmSettings algorithmSettings,
            IBoringWordsFilter boringWordsFilter,
            Palette palette)
        {
            this.image = image;
            this.algorithmSettings = algorithmSettings;
            this.palette = palette;
            this.presetsSettings = presetsSettings;
            this.rectangleBuilder = rectangleBuilder;
            this.pointFigure = pointFigure;
            this.boringWordsFilter = boringWordsFilter;
        }

        string IActionForm.Category => "Рисование";

        string IActionForm.Name => "Нарисовать";

        string IActionForm.Description => "Нарисовать облако тегов";

        void IActionForm.Perform()
        {
            algorithmSettings.ImagesDirectory = GetFilePathDialog();

            SettingsForm.For(algorithmSettings).ShowDialog();
            pointFigure.Reset();

            var cloud = new TagCloud();
            cloud.CreateTagCloud(
                pointFigure,
                rectangleBuilder,
                InitialTags(algorithmSettings.ImagesDirectory));

            var size = ImageSizer.GetImageSize(cloud.GetRectangles());
            image.RecreateImage(new ImageSettings { Height = size.Height, Width = size.Width });
            
            DrawCloud(cloud);
        }

        private string GetFilePathDialog()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.Filter = "Txt files (*.txt)|*.txt|Doc files (*.doc)|*.doc|Docx files (*.docx)|*.docx";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ShowDialog();
            return openFileDialog1.FileName;
        }

        private void DrawCloud(TagCloud cloud)
        {
            presetsSettings.Drawer.DrawCloudFromPalette(cloud.GetRectangles(), image,
                    palette);
        }

        public IEnumerable<ITag> InitialTags(string filePath)
        {
            string originalText = presetsSettings.Reader.ReadFile(filePath);

            var parsedText = presetsSettings.Parser.Parse(originalText);

            if (presetsSettings.Filtered == Switcher.Enabled)   
                parsedText = boringWordsFilter.FilterWords(parsedText);

            var formattedTags = presetsSettings.ToLowerCase == Switcher.Enabled
                ? presetsSettings.Formatter.ApplyFunction(parsedText, x => x.ToLower())
                : parsedText;

            var freqTags = presetsSettings.FrequencyCounter.GetTagsFrequency(formattedTags, presetsSettings.UseSort == Switcher.Enabled);

            return presetsSettings.FontSizer.GetTagsWithSize(freqTags, algorithmSettings.FontSettings);
        }
    }
}
