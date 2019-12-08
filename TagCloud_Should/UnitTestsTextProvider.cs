using System.Collections.Generic;
using System.Text;
using TagCloud.TextProvider;

namespace TagCloud_Should
{
    public class UnitTestsTextProvider : ITextProvider
    {
        public List<string> GetAllWords(IEnumerable<string> paths)
        {
            return new List<string>();
        }

        public Encoding TextEncoding { get; set; } = Encoding.UTF8;

        public List<string> GetAllWords()
        {
            return new List<string>
            {
                "word1",
                "than",
                "word1",
                "word1",
                "word2",
                "blacklistWord",
                "word3",
                "the",
                "am",
                "is",
                "are",
                "a",
                "b",
                "",
                " "
            };
        }
    }
}