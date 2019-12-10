using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudTextProcessing.Shufflers
{
    public class TokenShufflerRandom : ITokenShuffler

    {
        private readonly int randomSeed;
        public TokenShufflerRandom(int randomSeed) => this.randomSeed = randomSeed;

        public TokenShufflerRandom() => randomSeed = Environment.TickCount;

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