using TagsCloudVisualization;

namespace TagsCloud.Factories;

public static class CloudTagCreator
{
    public static List<CloudTag> CreateCloudTagList(CloudTagFactoryBase factory)
    {
        return factory
            .AdjustColors()
            .AdjustFonts()
            .AdjustPositions()
            .Build();
    }
}