using System.Drawing;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization
{
    public class CloudSaver : ISaver<IVisualizer<IWordsCloudBuilder>>
    {
        private readonly Bitmap bitmapFile;

        public CloudSaver(IVisualizer<IWordsCloudBuilder> visualizer)
        {
            this.bitmapFile = visualizer.Draw();
        }

        public void Save(string filename)
        {
            bitmapFile.Save(filename);
        }
    }
}
