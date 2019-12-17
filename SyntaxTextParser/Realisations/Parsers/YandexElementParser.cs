using System.Collections.Generic;
using System.Text.RegularExpressions;
using SyntaxTextParser.Architecture;
using SyntaxTextParser.YandexParser;

namespace SyntaxTextParser
{
    public class YandexElementParser : ElementParserWithRules
    {
        private readonly YandexToolUser toolUser;

        private static readonly Regex AnalysisRegex = new Regex(@"^\w+{(\w+)\??=(\w+)[,=]", RegexOptions.Compiled);

        public YandexElementParser(IEnumerable<IElementValidator> elementValidators,
            IElementFormatter elementFormatter, YandexToolUser toolUser) :
            base(elementValidators, elementFormatter)
        {
            this.toolUser = toolUser;
        }

        protected override IEnumerable<TypedTextElement> ParseText(string text)
        {
            foreach (var analysis in toolUser.ParseTextInTool(text))
            {
                var match = AnalysisRegex.Match(analysis);
                var initialForm = match.Groups[1].Value;
                var partOfSpeech = match.Groups[2].Value;
                if(string.IsNullOrEmpty(initialForm) 
                   || string.IsNullOrEmpty(partOfSpeech)) continue;

                yield return new TypedTextElement(initialForm, partOfSpeech, ElementFormatter);
            }
        }
    }
}