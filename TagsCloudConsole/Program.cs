using TagsCloudVisualization;
using System.Drawing;

namespace TagsCloudConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = new string[]
            {
                "hello", "hello", "world", "none", "hello"
            };

            var calculator = new StatisticsCalculator();
            var stat = calculator.CalculateStatistics(words);

            var tagsCloudGenerator = new TagsCloudGenerator(stat, new CircularCloudLayouter(new Point(0, 0)));
            var picture = TagsCloudVisualizer.GetPictureOfRectangles(tagsCloudGenerator.GenerateTagsCloud());
            picture.Save("sample.png");
        }
    }
}
