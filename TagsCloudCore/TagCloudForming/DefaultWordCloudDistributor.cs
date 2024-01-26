using TagsCloudCore.BuildingOptions;
using TagsCloudCore.Common;
using TagsCloudCore.Utils;
using TagsCloudCore.WordProcessing.WordGrouping;
using TagsCloudVisualization;

namespace TagsCloudCore.TagCloudForming;

public class DefaultWordCloudDistributor : IWordCloudDistributorProvider
{
    private readonly Dictionary<string, int> _words;
    private readonly ICloudLayouter _cloudLayouter;
    private readonly DrawingOptions _drawingOptions;

    public DefaultWordCloudDistributor(IProcessedWordProvider processedWord, ICommonOptionsProvider commonOptionsProvider,
        IDrawingOptionsProvider drawingOptionsProvider)
    {
        _words = processedWord.ProcessedWords;
        _cloudLayouter = commonOptionsProvider.CommonOptions.CloudLayouter;
        _drawingOptions = drawingOptionsProvider.DrawingOptions;
    }

    public IReadOnlyDictionary<string, WordData> DistributedWords => DistributeWords().AsReadOnly();

    private Dictionary<string, WordData> DistributeWords()
    {
        var distributed = new Dictionary<string, WordData>();

        foreach (var (word, frequency) in _words)
        {
            var newWord = new WordData(_cloudLayouter.PutNextRectangle(DrawingUtils.GetStringSize(word, frequency,
                _drawingOptions.FrequencyScaling, _drawingOptions.Font)), frequency);
            distributed.Add(word, newWord);
        }
        
        return distributed;
    }
}