using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var path = "hello.txt";
            var generator = new TagsCloudGenerator(new Size(16, 20));
            var parser = new Parser();
            var words = parser.ReadWords(path);
            var blackList = new HashSet<string>() {"в", "на", "к", "а", "не", "и", "с", "о"};
            var cloud = generator.CreateCloud(words,
                new BlacklistWordsFilter(blackList), new ToLowerCaseFormatter(),
                new CircularCloudLayouter(new Point(400, 400)));
            var renderer = new TagsCloudRenderer(FontFamily.GenericMonospace, Color.DarkBlue);
            renderer.RenderIntoFile("out.png", cloud, new Size(1280, 1024));
        }
    }
}