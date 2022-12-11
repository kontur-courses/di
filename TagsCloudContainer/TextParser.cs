using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class TextParser
    {
        private string[] boringWords = new []{"я"};

        public TextParser()
        {
            
        }
        public Dictionary<string, int> Parse(string text)
        {
            var required = new Dictionary<string, int>();
            text = text.ToLower();
            var words = new List<string>(
                text.Split().Where(o => !boringWords.Contains(o)));
            foreach (var word in words)
            {
                if (!required.Keys.Contains(word))
                    required[word] = 0;
                required[word]++;
            }

            return required;
        }
    }
}