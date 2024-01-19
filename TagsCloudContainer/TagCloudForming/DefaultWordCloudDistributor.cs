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

    public DefaultWordCloudDistributor(IWordGrouperProvider wordGrouper, ICloudLayouter cloudLayouter,
        IOptionsProvider optionsProvider)
    {
        _words = wordGrouper.GrouppedWords;
        _cloudLayouter = cloudLayouter;
        _options = optionsProvider.Options;
    }

    public IReadOnlyDictionary<string, Word> DistributedWords => DistributeWords().AsReadOnly();

    private Dictionary<string, Word> DistributeWords()
    {
        var distributed = new Dictionary<string, Word>();

        foreach (var (word, frequency) in _words)
        {
            var newWord = new Word(word, _cloudLayouter.PutNextRectangle(DrawingUtils.GetStringSize(word, frequency,
                _options.FrequencyScaling, _options.Font)), frequency);
            distributed.Add(word, newWord);
        }
        
        return distributed;
    }
}