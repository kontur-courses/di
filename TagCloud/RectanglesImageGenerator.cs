using System.IO;

namespace TagCloud
{
    public class RectanglesImageGenerator
    {
        private readonly ILayouter layouter;
        private readonly IReader fileReader;
        private readonly IDrawer drawer;

        public RectanglesImageGenerator(ILayouter layouter, IReader fileReader, IDrawer drawer)
        {
            this.layouter = layouter;
            this.fileReader = fileReader;
            this.drawer = drawer;
        }

        public void Generate(string fileName, string imageName)
        {
            var sizes = fileReader.Read(File.ReadAllLines(fileName));
            foreach (var size in sizes)
                layouter.PutNextRectangle(size);
            drawer.GenerateImage(layouter.Rectangles, imageName);
        }
    }
}