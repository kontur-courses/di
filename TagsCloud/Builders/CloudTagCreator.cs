using TagsCloudVisualization;

namespace TagsCloud.Builders;

public class CloudTagCreator
{
    public static List<CloudTag> CreateCloudTagList(CloudTagListBuilderBase builder)
    {
        return builder
               .AdjustColors()
               .AdjustFonts()
               .AdjustPositions()
               .Build();
    }
}