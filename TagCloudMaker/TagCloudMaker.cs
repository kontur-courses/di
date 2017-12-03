using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagCloud
{
    public class TagCloudMaker
    {
        public static IEnumerable<TextRectangle> GetTagCloudRectangles(IEnumerable<string> words, IEnumerable<string> badWords = null,
            bool fromLiterature = false)
        {
            //var checker = new NHunspell.Hunspell("en_us.aff", "en_us.dic");
            //var result = checker.Analyze("Mom");
            var cloudLayouter = new CircularCloudLayouter();
            var dict = words.Select(s => s.ToLower())
                .GroupBy(w => w)
                .OrderByDescending(g => g.Count())
                .ToDictionary(g => g.Key, g => g.Count());
            var tenPercent = dict.Select(p => p.Value).Max() * 10 / 100;
            foreach (var pair in dict)
            {
                var fontCoeff = Math.Max(1, pair.Value / tenPercent);
                var size = new Size(50 * fontCoeff, 50 * pair.Key.Length * fontCoeff);
                cloudLayouter.PutNextRectangle(size, pair.Key);
            }
            return cloudLayouter.CloudRectangles;
        }
    }
}