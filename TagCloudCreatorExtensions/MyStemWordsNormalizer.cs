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

    public IEnumerable<string> GetWordsOriginalForm(string sourceText) =>
        _grammarInfoParser.Parse(sourceText)
            .Select(grammarInfo => grammarInfo.OriginalForm);
}