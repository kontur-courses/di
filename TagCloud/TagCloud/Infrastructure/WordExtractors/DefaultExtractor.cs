using System;
using System.Linq;

namespace TagCloud
{
    public class DefaultExtractor : IExtractor
    {
        public string[] ExtractWords(string text)
        {
            if (text == null)
                throw new ArgumentNullException();
            var words = text.Split('\r', '\n');
            return words.Where(word => word != "").ToArray();
        }
    }
}
