using System;
using System.Drawing;
using TagsCloudVisualization.ColorGenerators;

namespace TagsCloudVisualization.TagsCloudDrawer.TagsCloudDrawerSettingsProvider
{
    public class TagsCloudDrawerSettingsProvider : ITagsCloudDrawerSettingsProvider
    {
        public Font Font { get; init; } = new(FontFamily.GenericMonospace, 14f);
        public IColorGenerator ColorGenerator { get; init; } = new RandomColorGenerator(new Random());
    }
}