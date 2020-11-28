using System;
using System.Collections.Generic;

namespace WordCloudGenerator
{
    public class Generator
    {
        private readonly Func<Dictionary<string, int>, Dictionary<string, int>> algorithm;

        public Generator(Func<Dictionary<string, int>, Dictionary<string, int>> algorithm = null)
        {
            this.algorithm = algorithm ?? DefaultAlgorithm;
        }

        private static Dictionary<string, int> DefaultAlgorithm(Dictionary<string, int> wordsCountDic)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, int> CalculateFontSizeForWords(Dictionary<string, int> wordsCount)
        {
            return algorithm(wordsCount);
        }
    }
}