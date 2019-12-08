using System.Collections.Generic;
using System.Linq;

namespace SyntaxTextParser.Architecture
{
    public abstract class TextElementParser
    {
        protected readonly IEnumerable<IElementValidator> elementValidators;

        protected TextElementParser(IEnumerable<IElementValidator> elementValidators)
        {
            this.elementValidators = elementValidators;
        }

        protected bool IsCorrectElement(string element)
        {
            return element != null && elementValidators.ToList()
                .TrueForAll(x => x.IsValidElement(element));
        }

        public abstract List<CountedTextElement> ParseElementsFromText(string text);
    }
}