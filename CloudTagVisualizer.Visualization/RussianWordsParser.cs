using System.Linq;
using System.Text.RegularExpressions;

namespace Visualization
{
    public class RussianWordsParser : IWordsParser
    {
        private static readonly Regex WordPattern = new Regex("[а-яА-ЯёЁ]+");
        public string[] Parse(string fullString)
        {
            return WordPattern
                .Matches(fullString)
                .Select(g => g.Value)
                .ToArray();
        }
    }
}