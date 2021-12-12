using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TokenGenerator
    {
        private readonly IWordSelector wordSelector;
        private readonly ITokenWeigher tokenWeigher;
        private readonly ITokenOrderer tokenOrderer;

        public TokenGenerator(IWordSelector wordSelector, ITokenWeigher tokenWeigher, ITokenOrderer tokenOrderer)
        {
            this.wordSelector = wordSelector;
            this.tokenWeigher = tokenWeigher;
            this.tokenOrderer = tokenOrderer;
        }

        public IEnumerable<Token> GetTokens(string text)
        {
            return tokenOrderer.OrderTokens(tokenWeigher.Evaluate(wordSelector.GetWords(text)));
        }
    }
}