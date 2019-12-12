using System.Collections.Generic;

namespace TagsCloudTextProcessing.WordsIntoTokensTranslators
{
    public interface IWordsIntoTokenTranslator
    {
        IEnumerable<Token> TranslateIntoTokens(IEnumerable<string> words);
    }
}