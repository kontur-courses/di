using TagCloud.Core.TextWorking;
using TagCloud.Core.Visualizers;

namespace TagCloud.Core
{
    public class TagCloud
    {
        private readonly TextWorker textWorker;
        private readonly ITagCloudVisualizer visualizer;

        public TagCloud(TextWorker textWorker, ITagCloudVisualizer visualizer)
        {
            this.textWorker = textWorker;
            this.visualizer = visualizer;
        }

        public void MakeTagCloud()
        {
            var tagStats = textWorker.GetTagStats();
            visualizer.Render(tagStats);
        }
    }
}
