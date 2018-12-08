using System.Drawing;

namespace TagsCloudVisualization
{
    public class GraphicsWriter
    {
        private readonly IDrawer drawer;

        public GraphicsWriter(IDrawer drawer)
        {
            this.drawer = drawer;
        }

        public void WriteToFile(string path)
        {
            using (var bitmap = new Bitmap(drawer.GetWidth(), drawer.GetHeight()))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                drawer.Draw(graphics);
                bitmap.Save(path);
            }
        }
    }
}
