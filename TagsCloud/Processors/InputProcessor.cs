using TagsCloud.Contracts;
using TagsCloud.Conveyors;
using TagsCloud.Entities;
using TagsCloud.TextAnalysisTools;
using TagsCloudVisualization;

namespace TagsCloud.Processors;

public class InputProcessor
{
    private readonly IFileReader[] fileReaders;
    private readonly FilterConveyor filterConveyor;
    private readonly IInputProcessorOptions inputOptions;
    private readonly IPostFormatter postFormatter;

    public InputProcessor(
        IInputProcessorOptions inputOptions,
        IPostFormatter postFormatter,
        IEnumerable<IFileReader> fileReaders,
        FilterConveyor filterConveyor)
    {
        this.inputOptions = inputOptions;
        this.postFormatter = postFormatter;
        this.filterConveyor = filterConveyor;
        this.fileReaders = fileReaders.ToArray();
    }

    public HashSet<WordTagGroup> CollectWordGroupsFromFile(string filename)
    {
        var extension = filename.Split('.', StringSplitOptions.RemoveEmptyEntries)[^1];
        var reader = FindAppropriateFileReader(extension);

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

    private IFileReader FindAppropriateFileReader(string extension)
    {
        return fileReaders.SingleOrDefault(reader => reader.SupportedExtension.Equals(extension));
    }
}