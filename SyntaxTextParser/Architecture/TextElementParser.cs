using System.Collections.Generic;
using System.Linq;

namespace SyntaxTextParser.Architecture
{
    public abstract class TextElementParser
    {
        protected readonly IEnumerable<IElementValidator> ElementValidators;
        protected readonly IElementPreformer ElementFormatter;

        protected TextElementParser(IEnumerable<IElementValidator> elementValidators, IElementPreformer elementFormatter)
        {
            ElementValidators = elementValidators;
            ElementFormatter = elementFormatter;
        }

        protected bool IsCorrectElement(TypedTextElement element)
        {
            return element != null && ElementValidators.ToList()
                .TrueForAll(x => x.IsValidElement(element));
        }

        public List<TextElement> ParseElementsFromText(string text)
        {
            var elements = new Dictionary<TypedTextElement, int>();

            foreach (var element in ParseText(text))
            {
                if(!IsCorrectElement(element)) continue;

                if (elements.ContainsKey(element))
                    elements[element]++;
                else
                    elements.Add(element, 1);
            }

            return elements
                .Select(x => x.Key.ConvertToTextElement(x.Value, ElementFormatter))
                .ToList();
        }

        protected abstract IEnumerable<TypedTextElement> ParseText(string text);
    }
}