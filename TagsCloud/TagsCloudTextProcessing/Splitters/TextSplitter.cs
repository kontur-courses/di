using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudTextProcessing.Splitters
{
    public class TextSplitter : ITextSplitter
    {
        private readonly Regex splitRegex;
        public TextSplitter(string splitPattern = @"\W+") => splitRegex = new Regex(splitPattern);

        public IEnumerable<string> SplitText(string text)
        {
            return splitRegex.Split(text).Where(w => w.Length > 0);
        }
    }
}