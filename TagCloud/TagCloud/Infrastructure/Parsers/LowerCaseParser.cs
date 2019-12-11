using System.Linq;

namespace TagCloud
{
    public class LowerCaseParser : IParser
    {
        public bool IsChecked { get; set; }

        public string Name { get; private set; }

        public LowerCaseParser()
        {
            IsChecked = true;
            Name = "LowerCase parser";
        }

        public string[] ParseWords(string[] words) => words
            .Select(word => word.ToLower())
            .ToArray();
    }
}
