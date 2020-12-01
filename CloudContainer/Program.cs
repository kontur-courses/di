using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;

namespace CloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "C:\\Users\\ivan0\\Desktop\\di\\CloudContainer\\bin\\Debug\\netcoreapp3.1\\text.txt"; //TODO
            var cleaner = new BoringWordsCleaner(new HashSet<string>());
            var provider = new TxtWordProvider(cleaner);
            var words = provider.GetWords(path);
            var config = new WordConfig(new Font(FontFamily.GenericMonospace, 20));
            var pointProvider = new PointProvider(new Point(500,500));//TODO fix
            var cloudLayouter = new CircularCloudLayouter(pointProvider);
            var converter = new WordsToRectanglesConverter(cloudLayouter, config);
            var cloudTags = converter.ConvertWords(words);
            var image = Drawer.DrawImage(cloudTags, new Point(500, 500));
            var imageSaver = new PngSaver();
            imageSaver.SaveImage(image, "newfile");
        }
    }
}