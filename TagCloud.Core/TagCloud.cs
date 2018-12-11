using System.Drawing;
using TagCloud.Core.Settings;
using TagCloud.Core.TextWorking;
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
            bitmap.Save(settings.PathForResultImage, settings.ImageFormat);
        }

        public Bitmap MakeTagCloud()
        {
            var tagStats = textWorker.GetTagStats(settings.PathToWords, settings.PathToBoringWords);
            return visualizer.Render(tagStats);
        }
    }
}
