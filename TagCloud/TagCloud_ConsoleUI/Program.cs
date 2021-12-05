using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud;
using TagCloud.Extensions;

namespace TagCloud_ConsoleUI
{
    public static class Program
    {
        public static void Main()
        {
            // DemoImageGenerator.GenerateCircularTagCloud(GetRandomSizesWithArea(3000, 600),
            //     new ArchimedeanSpiral());
            // DemoImageGenerator.GenerateCircularTagCloud(GetRandomSizesWithArea(300, 600),
            //     new ArchimedeanSpiral());
            // DemoImageGenerator.GenerateCircularTagCloud(GetRandomSizesWithArea(30, 600),
            //     new ArchimedeanSpiral());
            // DemoImageGenerator.GenerateCircularTagCloud(GetRandomSizesWithArea(
            //     new List<int> { 30, 300, 3000 }, 1200), new ArchimedeanSpiral());
            var processor = new TextProcessor();
            var words = processor.GetInterestingWords(@"DataSample.txt");
            var layouter = new CircularCloudLayouter(Point.Empty, new ArchimedeanSpiral());
            var g = Graphics.FromImage(new Bitmap(1, 1));
            var listWords = new List<Word>();
            foreach (var pair in words)
            {
                var word = pair.Key;
                var font = new Font(FontFamily.GenericSerif, pair.Value > 1 ? (int)(pair.Value * 14 * 0.6) : 14);
                var wordSize = g.MeasureString(word, font).ToSize();
                var wordRectangle = layouter.PutNextRectangle(wordSize);
                listWords.Add(new Word(word, font, wordRectangle));
            }

            var drawer = new TagCloudDrawer(Point.Empty);
            drawer.Draw(listWords).SaveDefault();
        }

        private static List<Size> GetRandomSizesWithArea(int area, int amount) =>
            GetRandomSizesWithArea(new List<int> { area }, amount);

        private static List<Size> GetRandomSizesWithArea(List<int> areas, int amount)
        {
            var sizes = new List<Size>();
            var rnd = new Random();

            for (int i = 0; i < amount; i++)
            {
                var area = areas[rnd.Next(areas.Count)];
                var height = rnd.Next((int)Math.Ceiling(Math.Pow(area, 0.3)), (int)Math.Pow(area, 0.5));
                var width = area / height;
                sizes.Add(new Size(width, height));
            }
            return sizes;
        }
    }
}
