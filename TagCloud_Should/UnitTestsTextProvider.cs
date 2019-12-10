using System.Collections.Generic;
using System.Text;
using TagCloud.TextProvider;

namespace TagCloud_Should
{
    public class UnitTestsTextProvider : ITextProvider
    {
        public List<string> GetAllLines(IEnumerable<string> paths)
        {
            return new List<string>();
        }

        public Encoding TextEncoding { get; set; } = Encoding.UTF8;

        public List<string> GetAllLines() =>
            new List<string>
            {
                "word1 word2, word.",
                "than;more : word",
                "word1 ^ word",
                "word1 word1 word1",
                "word2 word2 word2",
                "blacklistword blacklistword",
                "word3 blacklistword, word?",
                "the & word!",
                "am I word?",
                "is it unit test?",
                "are u mad???",
                "a b c d e f g",
                "h i j k l m n"
            };

        public List<string> GetLineWithSpaces() =>
            new List<string>
            {
                "sentence with spaces must be parsed correctly"
            };
        
        public List<string> GetLineWithPunctuationSigns() =>
            new List<string>
            {
                "sentence, with; punctuation signs. must: be parsed correctly! Isn't it?"
            };
    }
}