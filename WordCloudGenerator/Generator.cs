using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCloudGenerator
{
    public class Generator
    {
        private readonly Func<IEnumerable<WordFrequency>, IEnumerable<GraphicString>> algorithm;

        public Generator(Func<IEnumerable<WordFrequency>, IEnumerable<GraphicString>> algorithm)
        {
            this.algorithm = algorithm;
        }

        public IEnumerable<GraphicString> CalculateFontSizeForWords(IEnumerable<WordFrequency> wordFrequencies)
        {
            return algorithm(wordFrequencies);
        }
    }
}