using TagsCloudLayouter;

namespace TagCloud;

public static class TagCloudLayouterExtensions
{
    public static void PlaceTexts(this ICloudLayouter layouter, IEnumerable<Label> texts)
    {
        foreach (var text in texts)
            text.Location = layouter.PutNextRectangle(text.PreferredSize).Location;
    }
}