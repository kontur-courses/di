using System;
using System.Collections.Generic;
using System.Linq;
using SyntaxTextParser.Architecture;

namespace SyntaxTextParser
{
    public class LineTextElementParser : TextElementParser
    {
        public LineTextElementParser(IEnumerable<IElementValidator> elementValidators, IElementPreformer elementFormatter) :
            base(elementValidators, elementFormatter)
        { }

        protected override IEnumerable<TypedTextElement> ParseText(string text)
        {
            return text.Split()
                .Select(str => new TypedTextElement(str, GetElementType(str)));
        }

        protected string GetElementType(string element)
        {
            throw new NotImplementedException("Хз как библиотеку ту впихнуть");
        }
    }
}