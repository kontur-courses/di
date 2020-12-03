using System;
using System.Collections.Generic;

namespace WordCloudGenerator
{
    public class Generator
    {
        private readonly Func<IEnumerable<WordFrequency>, IEnumerable<GraphicString>> algorithm;

        public Generator(Func<IEnumerable<WordFrequency>, IEnumerable<GraphicString>> algorithm = null)
        {
            this.algorithm = algorithm ?? ExponentialAlgorithm;
        }

        private static IEnumerable<GraphicString> ExponentialAlgorithm(IEnumerable<WordFrequency> wordFrequencies)
        {
            var fontSize = 400f;

            foreach (var wordFrequency in wordFrequencies)
            {
                yield return new GraphicString(wordFrequency.Word, Math.Max(75, fontSize));
                fontSize /= 1.1f;
            }
        }

        public IEnumerable<GraphicString> CalculateFontSizeForWords(IEnumerable<WordFrequency> wordFrequencies)
        {
            return algorithm(wordFrequencies);
        }
    }
}