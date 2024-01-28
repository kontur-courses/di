using TagsCloud.Contracts;
using TagsCloud.Entities;

namespace TagsCloud.Builders;

public class InputOptionsBuilder
{
    private HashSet<string> excludedWords;
    private HashSet<string> languageParts;
    private bool onlyRussian;
    private bool toInfinitive;
    private CaseType wordsCase;

    public InputOptionsBuilder SetWordsCase(CaseType caseType)
    {
        wordsCase = caseType;
        return this;
    }

    public InputOptionsBuilder SetCastPolitics(bool caseToInfinitive)
    {
        toInfinitive = caseToInfinitive;
        return this;
    }

    public InputOptionsBuilder SetLanguagePolitics(bool russian)
    {
        onlyRussian = russian;
        return this;
    }

    public InputOptionsBuilder SetLanguageParts(IEnumerable<string> parts)
    {
        languageParts = parts.ToHashSet(StringComparer.OrdinalIgnoreCase);
        return this;
    }

    public InputOptionsBuilder SetExcludedWords(IEnumerable<string> excluded)
    {
        excludedWords = excluded.ToHashSet(StringComparer.OrdinalIgnoreCase);
        return this;
    }

    public IInputProcessorOptions BuildOptions()
    {
        return new InputProcessorOptions
        {
            ToInfinitive = toInfinitive,
            OnlyRussian = onlyRussian,
            LanguageParts = languageParts,
            WordsCase = wordsCase,
            ExcludedWords = excludedWords
        };
    }
}