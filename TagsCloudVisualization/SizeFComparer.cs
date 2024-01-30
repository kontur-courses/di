using SixLabors.ImageSharp;

namespace TagsCloudVisualization;

public class SizeFComparer : IComparer<SizeF>
{
    private readonly bool ascending;

    public SizeFComparer(bool ascending)
    {
        this.ascending = ascending;
    }

    public int Compare(SizeF first, SizeF second)
    {
        var square1 = (int)(first.Width * first.Height);
        var square2 = (int)(second.Width * second.Height);
        return ascending ? square1 - square2 : square2 - square1;
    }
}