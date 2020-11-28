using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using CircularCloudLayouter;

namespace WordCloudGenerator
{
    public class Program
    {
        public static void Main()
        {
            #region example

            //example (not works yet)
            var reader = new Reader();
            var text = reader.ReadFile("some path");
            
            var preparer = new Preparer(new []{"abc", "def", "xyz"});
            var prepared = preparer.GetCountedWords(text);
            
            // generator applies custom algorithms
            //
            // var algo = new Func<Dictionary<string, int>, Dictionary<string, int>>(preparedWords =>
            //     new Dictionary<string, int>());
            // var customGenerator = new Generator(algo);
            
            var generator = new Generator();
            var generated = generator.CalculateFontSizeForWords(prepared);
            
            var painter = new Painter(ImageFormat.Png, new CircularLayouter(new Point(600, 600)));
            painter.Paint(generated);
            painter.SaveImage("path to save");

            #endregion
            
        }
    }
}