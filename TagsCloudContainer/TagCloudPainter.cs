using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class TagCloudPainter
{
    private ICloudLayouter layouter;
    private Settings settings;

    public TagCloudPainter(ICloudLayouter layouter,
        Settings settings)
    {
        this.layouter = layouter;
        this.settings = settings;
    }

    public void Paint(IEnumerable<PaintedTag> tags)
    {
        throw new NotImplementedException();
    }
}