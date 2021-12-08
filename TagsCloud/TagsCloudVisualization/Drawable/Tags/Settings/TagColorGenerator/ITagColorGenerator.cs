using System.Drawing;

namespace TagsCloudVisualization.Drawable.Tags.Settings.TagColorGenerator
{
    public interface ITagColorGenerator
    {
        Color Generate(Tag tag);
    }
}