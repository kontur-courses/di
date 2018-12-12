using System.Drawing;
using System.Drawing.Imaging;
using TagCloud.Core.Settings;
using TagCloud.Core.TextWorking;
using TagCloud.Core.Util;
using TagCloud.Core.Visualizers;

namespace TagCloud.Core
{
    public class TagCloud
    {
        private readonly TextWorker textWorker;
        private readonly ITagCloudVisualizer visualizer;
        private readonly TagCloudSettings settings;

        public TagCloud(TextWorker textWorker, ITagCloudVisualizer visualizer, TagCloudSettings settings)
        {
            this.textWorker = textWorker;
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
            var tagStats = textWorker.GetTagStats(settings.PathToWords, settings.PathToBoringWords);
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
