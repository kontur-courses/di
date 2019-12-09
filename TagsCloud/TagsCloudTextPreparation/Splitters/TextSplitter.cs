using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudTextPreparation.Splitters
{
    public class TextSplitter : ITextSplitter
    {
        private readonly string splitPattern;
        public TextSplitter(string splitPattern = @"\W+") => this.splitPattern = splitPattern;

        public IEnumerable<string> SplitText(string text)
        {
            return Regex.Split(text, splitPattern).Where(w => w.Length > 0);;
        }
    }
}