using System.Collections.Generic;

namespace SyntaxTextParser.Architecture
{
    public abstract class BaseElementParser
    {
        protected readonly IElementFormatter ElementFormatter;

        protected BaseElementParser(IElementFormatter elementFormatter)
        {
            ElementFormatter = elementFormatter;
        }

        public abstract List<TextElement> ParseElementsFromText(string text);
    }
}