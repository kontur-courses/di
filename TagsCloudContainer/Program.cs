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
            var text = TextReader.GetTextFromFile($"{projectDirectory}\\TextFiles\\Example.txt");
            var handler = new WordHandler("BoringWords.txt");
            var settings = new Settings()
            {
                WordColor = Color.Purple,
                WordFontName = "Arial",
                WordFontSize = 16
            };
            var words = handler.ProcessWords(text, settings);
            var center = new Point(0, 0);
            var layouter = new CircularCloudLayouter(center);

            var visualisator = new RectangleVisualisator(words, layouter, settings);
            visualisator.Paint();
            visualisator.Save(projectDirectory + "\\Results", "Rectangles", ImageFormat.Png);
        }
    }
}