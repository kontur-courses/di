using System.Text.RegularExpressions;

namespace TagsCloudContainer
{
    public class WordsFilter : IWordsFilter
    {
        public List<string> FilterWords(List<string> taggedWords, List<string> boringWords,
            IMyConfiguration configuration)
        {
            var excludedParticalsArr =
                configuration.ExcludedParticals.Split(", ", StringSplitOptions.RemoveEmptyEntries);

            var excludedParts = string.Join('|', excludedParticalsArr
                .Select(x => "=(,|=)"
                    .Insert(1, x))
                .ToArray());
            excludedParts = excludedParts.Length == 0 ? "= (,|=)" : excludedParts;
            var regexString =
                "^(\\w+){((?!).)*$".Insert(11,
                    excludedParts); // something like that ^(\w+){((?!=SPRO(,|=)|=PR(,|=)|=PART(,|=)|=CONJ(,|=)).)*$
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

    public interface IWordsFilter
    {
        public List<string> FilterWords(List<string> taggedWords, List<string> boringWords,
            IMyConfiguration configuration);
    }
}