using System;
using System.Linq;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Bases;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGeneratorExtensions
{
    [Factorial("ReverseFrequencyWordsLayouter")]
    public class ReverseFrequencyWordsLayouter : WordsLayouterBase
    {
        private const int MaxSize = 300;
        private const int MinSize = 50;

        public ReverseFrequencyWordsLayouter(IRectanglesLayouter rectanglesLayouter, ISettings settings) :
            base(
                rectanglesLayouter,
                settings,
                e => e.OrderByDescending(p => p.freq),
                s => Math.Min((int)(MinSize * (double)s.maxFreq / s.freq), MaxSize))
        {}
    }
}