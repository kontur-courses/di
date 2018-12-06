using System.Collections.Generic;

namespace TagsCloudContainer.Input
{
    public class TxtParser : IWordParser
    {
        public IEnumerable<string> ParseWords(string input)
        {
            var words = input.Split();
            var result = new HashSet<string>();

            foreach (var word in words)
                result.Add(word);

            return result;
        }
    }
}
