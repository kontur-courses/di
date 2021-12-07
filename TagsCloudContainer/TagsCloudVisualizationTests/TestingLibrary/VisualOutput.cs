using System.Drawing;
using TagsCloudVisualizationTests.Interfaces;

namespace TagsCloudVisualizationTests.TestingLibrary
{
    public class VisualOutput
    {
        private readonly IVisualizer visualizer;

        public VisualOutput(IVisualizer visualizer)
        {
            this.visualizer = visualizer;
        }

        public void SaveToBitmap(string filename)
        {
            using var bitmap = DrawToBitmap();
            bitmap.Save(filename);
        }

        public Bitmap DrawToBitmap()
        {
            var size = visualizer.GetBitmapSize();
            var bitmap = new Bitmap(size.Width, size.Height);
            using var graphics = Graphics.FromImage(bitmap);
            visualizer.Draw(graphics);
            graphics.Save();
            return bitmap;
        }
    }
}