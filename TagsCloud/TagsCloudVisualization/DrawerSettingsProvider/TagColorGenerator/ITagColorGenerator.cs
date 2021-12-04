using System.Drawing;

namespace TagsCloudVisualization.DrawerSettingsProvider.TagColorGenerator
{
    public interface ITagColorGenerator
    {
        Color Generate(Tag tag);
    }
}