using System.Drawing;

public class TagCloud : ITagCloud
{
    private readonly IWordExtractor wordExtractor;
    private readonly IFrequencyDictionaryBuilder<string> frequencyBuilder;
    private readonly ITagCloudLayouter layouter;
    private readonly ITagCloudRenderer renderer;
    private readonly RenderOptions renderOptions;
    private readonly TagCloudOptions tagCloudOptions;

    public TagCloud(
        ITagCloudLayouter layouter,
        ITagCloudRenderer renderer,
        IWordExtractor wordExtractor,
        IFrequencyDictionaryBuilder<string> frequencyBuilder,
        RenderOptions renderOptions,
        TagCloudOptions tagCloudOptions)
    {
        this.layouter = layouter;
        this.renderer = renderer;
        this.wordExtractor = wordExtractor;
        this.frequencyBuilder = frequencyBuilder;
        this.renderOptions = renderOptions;
        this.tagCloudOptions = tagCloudOptions;
    }

    public Bitmap CreateCloud(string text)
    {
        var words = wordExtractor.Extract(text);

        var freqDict = frequencyBuilder.Build(words);

        return CreateCloud(freqDict);
    }

    public Bitmap CreateCloud(Dictionary<string, int> frequencyDict)
    {
        var frequencyPairs = frequencyDict.OrderByDescending(x => x.Value)
                                .ToArray();

        if (tagCloudOptions.MaxTagsCount != -1)
            frequencyPairs = frequencyPairs.Take(tagCloudOptions.MaxTagsCount).ToArray();

        var minFreq = frequencyPairs[^1].Value;
        var maxFreq = frequencyPairs[0].Value;

        var layouts = new List<WordLayout>();

        foreach (var pair in frequencyPairs)
        {
            // множитель на который умножается размер шрифта, в зависимости от частоты встречаемости слова. 
            var sizeMultiplier = TagCloudHelpers.GetMultiplier(pair.Value, minFreq, maxFreq);
            var fontSize = GetFontSize(sizeMultiplier, renderOptions.MinFontSize, renderOptions.MaxFontSize);

            var size = renderer.GetStringSize(pair.Key, fontSize);

            var rect = layouter.PutNextRectangle(size);

            layouts.Add(new WordLayout(pair.Key, rect, fontSize));
        }

        return renderer.Render(layouts.ToArray());
    }

    private int GetFontSize(double freqMultiplier, int minFontSize, int maxFontSize)
    {
        var size = minFontSize + (maxFontSize - minFontSize) * freqMultiplier;
        return (int)size;
    }
}