using Autofac;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.SettingsProviders;

namespace TagsCloudContainer.Defaults;

public class TextAnalyzer : ITextAnalyzer
{
    private readonly ITextReader[] textReaders;
    private readonly IWordNormalizer[] wordNormalizers;
    private readonly IWordFilter[] wordFilters;
    private readonly char[] wordSeparators;

    public TextAnalyzer(ITextReader[] textReaders, IWordNormalizer[] wordNormalizers, IWordFilter[] wordFilters, TextAnalyzerSettings settings) :
        this(textReaders, wordNormalizers, wordFilters, settings.WordSeparators)
    {
    }

    protected TextAnalyzer(ITextReader[] textReaders, IWordNormalizer[] wordNormalizers, IWordFilter[] wordFilters, char[] wordSeparators)
    {
        this.textReaders = textReaders;
        this.wordNormalizers = wordNormalizers;
        this.wordFilters = wordFilters;
        this.wordSeparators = wordSeparators;
    }

    public ITextStats AnalyzeText()
    {
        var result = new TextStats();
        foreach (var line in textReaders.SelectMany(x => x.ReadLines()))
        {
            var words = line
                .Split(wordSeparators)
                .Where(x => !string.IsNullOrWhiteSpace(x) && !x.All(y => !char.IsLetter(y)));
            foreach (var word in ApplyNormalizingAndFiltering(words))
            {
                result.UpdateWord(word);
            }
        }

        return result;
    }

    private IEnumerable<string> ApplyNormalizingAndFiltering(IEnumerable<string> words)
    {
        foreach (var normalizer in wordNormalizers)
        {
            words = words.Select(normalizer.Normalize).Where(x => !string.IsNullOrEmpty(x))!;
        }

        foreach (var filter in wordFilters)
        {
            words = words.Where(filter.IsValid);
        }

        return words;
    }
}
