using System.Collections.Generic;

namespace TagsCloudTextPreparation.Tokenizers
{
    public interface ITokenizer
    {
        IEnumerable<Token> Tokenize(IEnumerable<string> words);
    }
}