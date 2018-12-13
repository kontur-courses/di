using System.Drawing;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization
{
    public class CloudSaver : ISaver<Bitmap>
    {
        private readonly Bitmap bitmapFile;

        public CloudSaver(IVisualizer<Bitmap> visualizer)
        {
            this.bitmapFile = visualizer.Draw();
        }

        public void Save(string filename)
        {
            bitmapFile.Save(filename);
        }
    }
}
