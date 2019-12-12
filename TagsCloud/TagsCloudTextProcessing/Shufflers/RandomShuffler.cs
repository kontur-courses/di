using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudTextProcessing.Shufflers
{
    public class RandomShuffler : ITokenShuffler

    {
        private readonly int randomSeed;
        public RandomShuffler(int randomSeed)
        {
            this.randomSeed = randomSeed;
        }

        public IEnumerable<Token> Shuffle(IEnumerable<Token> tokens)
        {
            var random = new Random(randomSeed);
            var tokensList = tokens.ToList();
            while (tokensList.Count > 0)
            {
                var randomIndex = random.Next(0, tokensList.Count);
                yield return tokensList[randomIndex];
                tokensList.RemoveAt(randomIndex);
            }
        }
    }
}