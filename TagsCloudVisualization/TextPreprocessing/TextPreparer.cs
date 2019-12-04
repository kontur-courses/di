using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudVisualization
{
    public class TextPreparer
    {
        private readonly string text;

        public TextPreparer(string text)
        {
            this.text = text;
        }

        public IEnumerable<string> GetWords()
        {
            var words = Regex.Split(text, @"[ \n]");
            return words.Select(word => Regex.Replace(word, @"[^\w-]", string.Empty))
                .Where(preparedWord => preparedWord != string.Empty && preparedWord != "-");
        }
    }
}