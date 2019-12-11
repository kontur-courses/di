using System;
using System.Collections.Generic;
using System.Linq;
using SyntaxTextParser.Architecture;

namespace SyntaxTextParser
{
    public class LineElementParserWithRules : ElementParserWithRules
    {
        public LineElementParserWithRules(IEnumerable<IElementValidator> elementValidators, IElementFormatter elementFormatter) :
            base(elementValidators, elementFormatter)
        { }

        protected override IEnumerable<TypedTextElement> ParseText(string text)
        {
            return text.Split()
                .Select(str => new TypedTextElement(str, GetElementType(str), ElementFormatter));
        }

        protected string GetElementType(string element)
        {
            throw new NotImplementedException("Впихни библиотеку");
        }
    }
}