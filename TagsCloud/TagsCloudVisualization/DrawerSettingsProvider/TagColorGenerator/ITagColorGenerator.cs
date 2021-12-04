using System.Drawing;

namespace TagsCloudVisualization.DrawerSettingsProvider
{
    public interface ITagColorGenerator
    {
        Color Generate(Tag tag);
    }
}