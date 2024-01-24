using TagsCloudContainer.Common;
using TagsCloudContainer.DrawingOptions;
using TagsCloudContainer.Utils;
using TagsCloudContainer.WordProcessing.WordGrouping;
using TagsCloudVisualization;

namespace TagsCloudContainer.TagCloudForming;

public class DefaultWordCloudDistributor : IWordCloudDistributorProvider
{
    private readonly Dictionary<string, int> _words;
    private readonly ICloudLayouter _cloudLayouter;
    private readonly Options _options;

    public DefaultWordCloudDistributor(IProcessedWordProvider processedWord, ICloudLayouter cloudLayouter,
        IOptionsProvider optionsProvider)
    {
        _words = processedWord.ProcessedWords;
        _cloudLayouter = cloudLayouter;
        _options = optionsProvider.Options;
    }

    public IReadOnlyDictionary<string, WordData> DistributedWords => DistributeWords().AsReadOnly();

    private Dictionary<string, WordData> DistributeWords()
    {
        var distributed = new Dictionary<string, WordData>();

        foreach (var (word, frequency) in _words)
        {
            var newWord = new WordData(_cloudLayouter.PutNextRectangle(DrawingUtils.GetStringSize(word, frequency,
                _options.FrequencyScaling, _options.Font)), frequency);
            distributed.Add(word, newWord);
        }
        
        return distributed;
    }
}