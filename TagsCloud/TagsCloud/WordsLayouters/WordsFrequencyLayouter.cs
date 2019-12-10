using System;
using System.Linq;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Bases;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.WordsLayouters
{
    [Factorial("WordsFrequencyLayouter")]
    public class WordsFrequencyLayouter : WordsLayouterBase
    {
        private const int MaxSize = 300;
        private const int MinSize = 10;

        public WordsFrequencyLayouter(IRectanglesLayouter rectanglesLayouter, ISettings settings) : 
            base(
                rectanglesLayouter,
                settings,
                e => e.OrderByDescending(p => p.freq),
                s => Math.Max((int)(MaxSize * (double)s.freq / s.maxFreq), MinSize))
        {}
    }
}