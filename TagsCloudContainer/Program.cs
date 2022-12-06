using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var text = TextReader.GetTextFromFile("D:\\Documents\\Kontur\\di\\TagsCloudContainer\\Example.txt");
            var handler = new WordHandler();
            var words = handler.ProcessWords(text);
            
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
            
            // var center = new Point(0, 0);
            // var layouter = new CircularCloudLayouter(center);
            // var rectangles = GenerateRectangles(50, layouter);
            //
            // RectangleVisualisator visualisator = new RectangleVisualisator(rectangles);
            // visualisator.Paint();
            // visualisator.Save("Rectangles", ImageFormat.Png);
        }

        public static List<Rectangle> GenerateRectangles(int amount, CircularCloudLayouter layouter)
        {
            if (amount <= 0)
                throw new ArgumentException();

            var random = new Random(1);
            List<Rectangle> rectangles = new List<Rectangle>();
            for (int i = 0; i < amount; i++)
            {
                var size = new Size(random.Next() % 80 + 40, random.Next() % 20 + 20);
                rectangles.Add(layouter.PutNextRectangle(size));
            }

            return rectangles;
        }
    }
}
 