using System.Collections.Generic;
using TagsCloudContainerCore.Console;
using TagsCloudContainerCore.InterfacesCore;

namespace TagsCloudContainerCore;

public class TagCloudMaker : ITagCloudMaker<LayoutSettings>
{
    private readonly IStatisticMaker statisticMaker;
    private readonly ITagMaker<LayoutSettings> tagMaker;


    public TagCloudMaker(
        IStatisticMaker statisticMaker,
        ITagMaker<LayoutSettings> tagMaker)
    {
        this.statisticMaker = statisticMaker;
        this.tagMaker = tagMaker;
    }

    public IEnumerable<TagToRender> GetTagsToRender(IEnumerable<string> tags, LayoutSettings settings)
    {
        statisticMaker.AddTagValues(tags);

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var rawTag in statisticMaker.CountedTags)
        {
            var tag = tagMaker.MakeTag(rawTag, settings, statisticMaker);

            yield return tag;
        }
    }
}