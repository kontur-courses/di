using System.Text.RegularExpressions;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class WordsFilter : IWordsFilter
    {
        public List<string> FilterWords(List<string> taggedWords, List<string> boringWords,
            ICustomOptions options)
        {
            //PoS - Part of Speech; grammemes - grammatical number etc
            var excludedPoS =
                options.ExcludedParticals.Split(", ", StringSplitOptions.RemoveEmptyEntries);

            var jointPos = string.Join('|', excludedPoS
                .Select(x => "=(,|=)"
                    .Insert(1, x))
                .ToArray());
            jointPos = jointPos.Length == 0 ? "= (,|=)" : jointPos;
            var regexString =
                "^(\\w+){((?!).)*$".Insert(11,
                    jointPos); // something like that ^(\w+){((?!=SPRO(,|=)|=PR(,|=)|=PART(,|=)|=CONJ(,|=)).)*$
            var regex = new Regex(regexString);

            var inputWords = taggedWords
                .Where(x => regex.IsMatch(x))
                .Select(x =>
                {
                    var match = regex.Match(x);
                    return match.Groups[1].Value.ToLower();
                }).ToList();

            return inputWords.Where(x => !boringWords.Contains(x)).ToList();
        }
    }
}