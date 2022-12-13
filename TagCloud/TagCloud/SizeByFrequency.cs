namespace TagCloud;

public class SizeByFrequency
{
    public SizeByFrequency(FontProperties fontProperties)
    {
        FontProperties = fontProperties;
    }

    private FontProperties FontProperties { get; }

    public List<Label> ResizeAll(Dictionary<string, int> wordsWithSize)
    {
        var maxCount = wordsWithSize.Values.Max();
        var texts = new List<Label>();
        foreach (var word in wordsWithSize)
        {
            var text = new Label();
            text.Text = word.Key;
            text.TextAlign = FontProperties.TextAlign;
            var size = ComputeFontSize(word.Value, maxCount, FontProperties.MinSize, FontProperties.MaxSize);
            text.Font = new Font(FontProperties.Family, size, FontProperties.Style);
            text.AutoSize = true;
            texts.Add(text);
        }

        return texts;
    }

    private static float ComputeFontSize(float count, float maxCount, float minSize, float maxSize)
    {
        return minSize + count / maxCount * (maxSize - minSize);
    }
}