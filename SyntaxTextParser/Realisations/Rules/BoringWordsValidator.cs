using System.Collections.Generic;
using SyntaxTextParser.Architecture;

namespace SyntaxTextParser
{
    public class BoringWordsValidator : IElementValidator
    {
        private readonly HashSet<string> boringWords;

        public BoringWordsValidator(params string[] words)
        {
            boringWords = new HashSet<string>(words);
        }

        public bool IsValidElement(TypedTextElement element)
        {
            return !boringWords.Contains(element.Word);
        }
    }
}