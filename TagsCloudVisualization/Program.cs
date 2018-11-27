using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class Program
    {
        static void Main(string[] args)
        {

            DrawDefaultRectangleCloud();
            DrawDefaultTagCloud();
        }

        public static void DrawDefaultRectangleCloud()
        {
            var random = new Random();
            var rectangles = new List<Rectangle>();
            var layout = new CircularCloudLayouter();
            rectangles.Add(layout.PutNextRectangle(new Size(800, 200)));
            for (var i = 0; i < 100; i++)
            {
                var randomSize = new Size(random.Next(150, 200), random.Next(75, 100));
                var newRectangle = layout.PutNextRectangle(randomSize);
                rectangles.Add(newRectangle);
            }
            var visualizer = new CircularCloudVisualizer(rectangles);
            var bitmap = visualizer.GetTagCloudImage();
            bitmap.Save("100_rectangles.png");
        }

        public static void DrawDefaultTagCloud()
        {
            var layout = new CircularCloudLayouter();
            var text =
                "So I said yes to Thomas Clinton and later thought that I had said yes to God and later still realized I had said yes only to Thomas Clinton";
            var analyzer = new WordAnalyzer();
            var parsedText = analyzer.TextAnalyzer(text);
            var weightedWords = analyzer.WeightWords(parsedText);
            var tagLayout = new TagCloudLayouter(layout, weightedWords);
            var tags = tagLayout.GetTags();
            var visualizer = new TagCloudVisualization(tags);
            var bitmap = visualizer.GetTagCloudImage();
            bitmap.Save("tag_cloud.png");
        }
    }
}
