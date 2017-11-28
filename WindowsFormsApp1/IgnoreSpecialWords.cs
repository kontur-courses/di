using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    public class IgnoreSpecialWords : ITagFilter
    {
        private HashSet<string> SpecialStrings { get; }

        public IgnoreSpecialWords( ITextReader reader, IgnoreWordsFiles ignoreWords)
        {
            SpecialStrings = new HashSet<string>();

            foreach (var path in ignoreWords.Paths)
            {
                foreach (var ignoreWord in reader.Read(path))
                {
                    SpecialStrings.Add(ignoreWord);
                }
            }

        }

        public IEnumerable<string> Filter(IEnumerable<string> tags)
        {
            return tags.Where(tag => !SpecialStrings.Contains(tag));
        }
    }

    public class IgnoreWordsFiles
    {
        public string[] Paths;

    }

}