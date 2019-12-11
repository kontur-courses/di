using System.Collections.Generic;
using TagCloud.TextProvider;

namespace TagCloud_Should
{
    public class UnitTestsTextProvider : ITextProvider
    {
        public List<string> GetAllLines(IEnumerable<string> paths)
        {
            return new List<string>();
        }

        public List<string> GetAllLines()
        {
            return new List<string>
            {
                "wOrd1 Word2, word.",
                "tHan;more : word",
                "word1 ^ word",
                "WORD1 word1 word1",
                "word2 WORD2 wORd2",
                "blacklistword blacklistword",
                "word3 blacklistword, word?",
                "the & worD!",
                "am I wOrd?",
                "Is It UnIt TesT?",
                "are u mad???",
                "a b c D e f g",
                "h i j k l m n"
            };
        }

        public List<string> GetLineWithSpaces()
        {
            return new List<string>
            {
                "sentence with spaces must be parsed correctly"
            };
        }

        public List<string> GetLineWithPunctuationSigns()
        {
            return new List<string>
            {
                "sentence, with; punctuation signs. must: be parsed correctly! Isn't it?"
            };
        }
    }
}