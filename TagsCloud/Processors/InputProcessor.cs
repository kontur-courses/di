using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Contracts;
using TagsCloud.Conveyors;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;
using TagsCloud.TextAnalysisTools;
using TagsCloudVisualization;

namespace TagsCloud.Processors;

[Injection(ServiceLifetime.Singleton)]
public class InputProcessor : IInputProcessor
{
    private readonly IEnumerable<IFileReader> fileReaders;
    private readonly FilterConveyor filterConveyor;
    private readonly IInputProcessorOptions inputOptions;
    private readonly IPostFormatter postFormatter;

    public InputProcessor(
        IInputProcessorOptions inputOptions,
        IEnumerable<IFileReader> fileReaders,
        IEnumerable<IFilter> filters,
        IPostFormatter postFormatter)
    {
        this.inputOptions = inputOptions;
        this.fileReaders = fileReaders;
        this.postFormatter = postFormatter;
        filterConveyor = new FilterConveyor(filters, inputOptions);
    }

    public HashSet<WordTagGroup> CollectWordGroupsFromFile(string filename)
    {
        var reader = FindFileReader(filename);
        var wordGroups = reader
                         .ReadContent(filename, postFormatter)
                         .Where(line => !string.IsNullOrEmpty(line))
                         .GroupBy(line => line)
                         .Select(group => new WordTagGroup(group.Key, group.Count()))
                         .ToHashSet();

        if (wordGroups.Count == 0)
            throw new ArgumentException("No words found! Check file structure.");

        TextAnalyzer.FillWithAnalysis(wordGroups);
        filterConveyor.ApplyFilters(wordGroups);

        CastWordsToAppropriateForm(wordGroups);
        CastWordsToAppropriateCase(wordGroups);

        wordGroups = wordGroups
                     .GroupBy(group => group.WordInfo.Text)
                     .Select(group =>
                         new WordTagGroup(group.Key, group.Sum(tag => tag.Count)))
                     .ToHashSet();

        return wordGroups;
    }

    private void CastWordsToAppropriateForm(HashSet<WordTagGroup> wordGroups)
    {
        foreach (var group in wordGroups.Where(group => group.WordInfo.IsRussian))
            group.WordInfo.Text = inputOptions.ToInfinitive ? group.WordInfo.Infinitive : group.WordInfo.Text;
    }

    private void CastWordsToAppropriateCase(HashSet<WordTagGroup> wordGroups)
    {
        foreach (var group in wordGroups)
            group.WordInfo.Text = inputOptions.WordsCase == CaseType.Upper
                ? group.WordInfo.Text.ToUpper()
                : group.WordInfo.Text.ToLower();
    }

    private static string GetFileExtension(string filename)
    {
        return filename.Split('.', StringSplitOptions.RemoveEmptyEntries)[^1];
    }

    private string GetSupportedExtensions()
    {
        return string.Join(", ", fileReaders.Select(reader => reader.SupportedExtension));
    }

    private IFileReader FindFileReader(string filename)
    {
        var extension = GetFileExtension(filename);
        var reader = fileReaders.SingleOrDefault(reader => reader.SupportedExtension.Equals(extension));

        if (reader != null)
            return reader;

        var extensions = GetSupportedExtensions();

        throw new NotSupportedException(
            $"Unknown file extension! Got {extension}, but candidates are: {extensions}");
    }
}