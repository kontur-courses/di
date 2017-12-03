using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NHunspell;

namespace TagCloudMaker
{
    public class TagCloudMaker
    {
        public static IEnumerable<TextRectangle> MakeTagCloud(string fileName)
        {
            var file = File.ReadLines(fileName);
            var checker = new NHunspell.Hunspell("en_us.aff", "en_us.dic");
            var result = checker.Analyze("Mom");
            var cloudLayouter = new CircularCloudLayouter();
            return new[] {new TextRectangle(new Point(0, 0), new Size(0, 0), "")};
        }
    }
}