using TagsCloudLayouter;

namespace TagCloud;

public static class TagCloudLayouterExtensions
{
    public static void PlaceTexts(this ICloudLayouter layouter, IEnumerable<TextBox> textBoxes)
    {
        foreach (var textBox in textBoxes)
            textBox.Location = layouter.PutNextRectangle(textBox.PreferredSize).Location;
    }
}