using System.Collections.Generic;
using System.Linq;

namespace SyntaxTextParser.Architecture
{
    public abstract class TextElementParser
    {
        protected readonly IEnumerable<IElementValidator> ElementValidators;

        protected TextElementParser(IEnumerable<IElementValidator> elementValidators)
        {
            ElementValidators = elementValidators;
        }

        protected bool IsCorrectElement(string element)
        {
            return element != null && ElementValidators.ToList()
                .TrueForAll(x => x.IsValidElement(element));
        }

        public abstract List<CountedTextElement> ParseElementsFromText(string text);
    }
}