using SixLabors.ImageSharp;
using TagsCloudContainer.Export;
using TagsCloudContainer.Load;
using TagsCloudContainer.Processing;
using TagsCloudContainer.Render;

namespace TagsCloudContainer;

public class TagsCloudGenerator
{
    private readonly ICloudRender _render;
    private readonly IWordsProcessor _wordsProcessor;
    private readonly IWordsLoader _wordsLoader;
    private readonly ICloudExporter _exporter;

    public TagsCloudGenerator(ICloudRender render, IWordsProcessor wordsProcessor, IWordsLoader wordsLoader,
        ICloudExporter exporter)
    {
        _render = render;
        _wordsProcessor = wordsProcessor;
        _wordsLoader = wordsLoader;
        _exporter = exporter;
    }

    public async Task<Image> GenerateAsync(CancellationToken cancellationToken = default)
    {
        // preprocess
        var processedWords = new List<string>();
        foreach (var word in await _wordsLoader.GetWordsAsync(cancellationToken))
        {
            if (_wordsProcessor.TryProcess(word, out var processedWord))
            {
                processedWords.Add(processedWord);
            }
        }

        // group and count
        (string Word, int Count)[] wordsByCount = processedWords.GroupBy(word => word)
            .Select(grouping => (grouping.Key, grouping.Count())).ToArray();

        // render
        var image = _render.Render(wordsByCount);

        return image;
    }

    public async Task GenerateAndExportAsync(CancellationToken cancellationToken = default)
    {
        var image = await GenerateAsync(cancellationToken);
        await ExportAsync(image, cancellationToken);
    }

    public async Task ExportAsync(Image image, CancellationToken cancellationToken = default)
    {
        await _exporter.ExportAsync(image, cancellationToken);
    }
}