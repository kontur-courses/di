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
            var normalizer = new WordNormalizer();
            var analyzer = new TextAnalyzer.StandardAnalyzer(normalizer);
            var layouter = new CircularCloudLayouter(new Point(30, 30));
            var tagCloud = new TagCloud(layouter, new TextFileReader(), analyzer);
            tagCloud.GetWords("");
        }
    }
}