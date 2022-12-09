using TagsCloudLayouter;

namespace TagCloud;

public class TextWrapper
{
    private ICloudLayouter Layouter { get; set; }
    private FontProperties FontProperties { get; set; }
    
    public TextWrapper(
        ICloudLayouter layouter, 
        FontProperties fontProperties)
    {
        Layouter = layouter;
        FontProperties = fontProperties;
    }

    public IEnumerable<TextBox> Wrap(Dictionary<string, int> wordsWithSize)
    {
        var maxCount = wordsWithSize.Values.Max();
        var textBoxes = new List<TextBox>();
        foreach (var word in wordsWithSize)
        {
            var text = new TextBox();
            text.Text = word.Key;
            text.TextAlign = FontProperties.TextAlign;
            var size = ComputeFontSize(word.Value, maxCount, FontProperties.MinSize, FontProperties.MaxSize);
            text.Font = new Font(FontProperties.Family, size, FontProperties.Style);
            textBoxes.Add(text);
        }
        return textBoxes;
    }
    
    private static float ComputeFontSize(float count, float maxCount, float minSize, float maxSize) =>
        minSize + count / maxCount * (maxSize - minSize);
}