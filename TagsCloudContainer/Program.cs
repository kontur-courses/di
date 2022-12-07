using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var text = TextReader.GetTextFromFile($"{projectDirectory}\\Example.txt");
            var handler = new WordHandler();
            var words = handler.ProcessWords(text);

            var center = new Point(0, 0);
            var layouter = new CircularCloudLayouter(center);
            
            RectangleVisualisator visualisator = new RectangleVisualisator(words, layouter);
            visualisator.Paint();
            visualisator.Save("Rectangles", ImageFormat.Png);
        }
    }
}
 