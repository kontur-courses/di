using System.Collections.Generic;

namespace TagCloud
{
    public class TextPreparerSettings
    {
        public string FilePath { get; set; } = "input.txt";

        public readonly HashSet<string> RusWordsBlackList = new HashSet<string>
        {
            "По",
            "А",
            "На",
            "В"
        };

        public readonly HashSet<string> EngWordsBlackList = new HashSet<string>
        {
            "",
            "A",
            "Of",
            "Is",
            "Are",
            "In",
            "To",
            "And",
            "The",
            "On",
            "By",
            "As",
            "Or",
            "For",
            "Now",
            "The",
            "From",
            "Only",
            "Was",
            "Into",
            "Their"
        };
    }
}