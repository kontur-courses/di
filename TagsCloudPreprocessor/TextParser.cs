using System.Collections.Generic;
using System.Linq;

namespace TagsCloudPreprocessor
{
    public class TextParser : ITextParser
    {
        public IEnumerable<string> GetWords(string text)
        {
            var separators = new[] { ' ', '\n' };
            var letters = 
                string.Join("", text.
                    Where(c => char.IsLetter(c) || separators.Contains(c)));

            return letters
                .Split(separators)
                .Select(w => w.ToLower());
        }
    }
}
