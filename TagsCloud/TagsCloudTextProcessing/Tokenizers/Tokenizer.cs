using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudTextProcessing.Tokenizers
{
    public class Tokenizer : ITokenizer
    {
        private readonly Regex splitRegex;
        public Tokenizer(string splitPattern = @"\W+") => splitRegex = new Regex(splitPattern);

        public IEnumerable<string> Tokenize(string text)
        {
            return splitRegex.Split(text).Where(w => w.Length > 0);
        }
    }
}