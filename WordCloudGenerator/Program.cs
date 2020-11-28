using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace WordCloudGenerator
{
    public class Program
    {
        public static void Main()
        {
            #region example

            // //example (not works yet)
            // var reader = new Reader();
            // var text = reader.ReadFile("some path");
            //
            // var preparer = new Preparer(new []{"abc", "def", "xyz"});
            // var prepared = preparer.GetCountedWords(text);
            //
            // // generator applies custom algorithms
            // //
            // // var algo = new Func<Dictionary<string, int>, Dictionary<string, int>>(preparedWords =>
            // //     new Dictionary<string, int>());
            // // var customGenerator = new Generator(algo);
            //
            // var generator = new Generator();
            // var generated = generator.CalculateFontSizeForWords(prepared);
            //
            // var painter = new Painter(ImageFormat.Png, new CircularLayouter(new Point(600, 600)));
            // painter.Paint(generated);
            // painter.SaveImage("path to save");

            #endregion

            var words = new Dictionary<string, int>
                {["apple"] = 30, ["ss"] = 20, ["tomatos"] = 15, ["help"] = 10, ["rap"] = 9, ["ss"] = 8};
            var painter = new Painter(ImageFormat.Png, new CircularLayouter.CircularLayouter(new Point(300, 300)),
                FontFamily.GenericSansSerif, new Palette());
            var img = painter.Paint(words);
            painter.SaveImage(img, "sb.png");
        }
    }
}