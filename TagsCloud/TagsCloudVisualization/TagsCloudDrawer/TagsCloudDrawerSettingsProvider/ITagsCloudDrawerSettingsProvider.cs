using System.Drawing;
using TagsCloudVisualization.ColorGenerators;

namespace TagsCloudVisualization.TagsCloudDrawer.TagsCloudDrawerSettingsProvider
{
    public interface ITagsCloudDrawerSettingsProvider
    {
        Font Font { get; }
        IColorGenerator ColorGenerator { get; }
    }
}