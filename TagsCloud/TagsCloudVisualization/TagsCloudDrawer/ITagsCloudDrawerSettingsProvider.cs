using System.Drawing;
using TagsCloudVisualization.ColorGenerators;

namespace TagsCloudVisualization.TagsCloudDrawer
{
    public interface ITagsCloudDrawerSettingsProvider
    {
        FontFamily FontFamily { get; }
        IColorGenerator ColorGenerator { get; }
    }
}