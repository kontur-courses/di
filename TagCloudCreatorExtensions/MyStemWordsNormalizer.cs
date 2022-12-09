using System.Text.RegularExpressions;
using MyStemWrapper;
using TagCloudCreator.Interfaces;

namespace TagCloudCreatorExtensions;

public class MyStemWordsNormalizer : IWordsNormalizer
{
    private readonly MyStemWordsGrammarInfoParser _grammarInfoParser;

    public MyStemWordsNormalizer(MyStemWordsGrammarInfoParser grammarInfoParser)
    {
        _grammarInfoParser = grammarInfoParser;
    }

    public IEnumerable<string> GetWordsOriginalForm(string sourceText)
    {
        var sourceWords = Regex.Split(sourceText.ToLower(), @"\W+")
            .Where(word => !string.IsNullOrWhiteSpace(word));
        return _grammarInfoParser.Parse(sourceWords)
            .Select(grammarInfo => grammarInfo.OriginalForm);
    }
}