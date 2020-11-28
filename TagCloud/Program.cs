using System;
using System.Drawing;
using TagCloud.FileReaders;
using TagCloud.Layouters;
using TagCloud.TextAnalyzer.WordNormalizer;

namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: DI!!!!!!!
            var normalizer = new WordNormalizer();
            var analyzer = new TextAnalyzer.StandardAnalyzer(normalizer);
            var layouter = new CircularCloudLayouter(new Point(1000, 1000));
            var tagCloud = new TagCloud(layouter, new TextFileReader(), analyzer);
            tagCloud.MakeTagCloud("ВСТАВЬТЕ СЮДА ПУТЬ ДО ФАЙЛА");
        }
    }
}