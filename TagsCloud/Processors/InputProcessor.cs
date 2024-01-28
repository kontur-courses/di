using TagsCloud.Contracts;
using TagsCloud.Conveyors;
using TagsCloud.Entities;
using TagsCloud.TextAnalysisTools;
using TagsCloudVisualization;

namespace TagsCloud.Processors;

public class InputProcessor
{
    private readonly IInputProcessorOptions inputOptions;
    private readonly IEnumerable<IFileReader> fileReaders;
    private readonly IPostFormatter postFormatter;
    private readonly FilterConveyor filterConveyor;

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
        var extension = filename.Split('.', StringSplitOptions.RemoveEmptyEntries)[^1];
        var reader = FindFileReader(extension);

        if (reader == null)
        {
            var extensions = GetSupportedExtensions();
            throw new NotSupportedException(
                $"Unknown file extension! Got {extension}, but candidates are: {extensions}");
        }

        var wordGroups = reader
                         .ReadContent(filename, postFormatter)
                         .GroupBy(line => line)
                         .Select(group => new WordTagGroup(group.Key, group.Count()))
                         .ToHashSet();

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

    private string GetSupportedExtensions()
    {
        return string.Join(", ", fileReaders.Select(reader => reader.SupportedExtension));
    }

    private IFileReader FindFileReader(string extension)
    {
        return fileReaders.SingleOrDefault(reader => reader.SupportedExtension.Equals(extension));
    }
}