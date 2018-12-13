using System.Drawing;
using System.Drawing.Imaging;
using TagCloud.Core.Settings;
using TagCloud.Core.Settings.DefaultImplementations;
using TagCloud.Core.Settings.Interfaces;
using TagCloud.Core.TextParsing;
using TagCloud.Core.Util;
using TagCloud.Core.Visualizers;

namespace TagCloud.Core
{
    public class TagCloud
    {
        private readonly WordsParser wordsParser;
        private readonly ITagCloudVisualizer visualizer;
        private readonly ITagCloudSettings settings;

        public TagCloud(WordsParser wordsParser, ITagCloudVisualizer visualizer, ITagCloudSettings settings)
        {
            this.wordsParser = wordsParser;
            this.visualizer = visualizer;
            this.settings = settings;
        }

        public void MakeTagCloudAndSave()
        {
            var bitmap = MakeTagCloud();
            Save(bitmap);
        }

        public Bitmap MakeTagCloud()
        {
            var tagStats = wordsParser.Parse(settings.PathToWords, settings.PathToBoringWords);
            return visualizer.Render(tagStats);
        }

        public void Save(Image image)
        {
            var imageFormat = ImageFormatResolver.TryResolveFromFileName(settings.PathForResultImage, out var res)
                ? res
                : ImageFormat.Png;
            image.Save(settings.PathForResultImage, imageFormat);
        }
    }
}
