using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainerCore.InterfacesCore;

namespace TagsCloudContainerCore;

public class CloudLayouter
{
    public readonly int BackgroundColorHex;

    private readonly IStatisticMaker statisticMaker;
    private readonly ILayouter cloudLayouter;
    private readonly ITagMaker tagMaker;

    private readonly string fontName;
    private readonly int fontColorHex;
    private readonly float maxFontSize;
    private readonly Size imageSize;


    public CloudLayouter(
        IStatisticMaker statisticMaker,
        ILayouter cloudLayouter,
        ITagMaker tagMaker,
        Size imageSize,
        string fontName,
        float maxFontSize,
        int fontColorHex,
        int backgroundColorHex)
    {
        BackgroundColorHex = backgroundColorHex;
        this.statisticMaker = statisticMaker;
        this.tagMaker = tagMaker;
        this.imageSize = imageSize;
        this.fontColorHex = fontColorHex;
        this.fontName = fontName;
        this.cloudLayouter = cloudLayouter;
        this.maxFontSize = maxFontSize;
    }

    public IEnumerable<TagToRender> GetTagsToRender(IEnumerable<string> tags)
    {
        statisticMaker.AddTags(tags);
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var tag in statisticMaker.CountedTags)
        {
            var fontSize = tagMaker.GetFontSize(tag, statisticMaker, maxFontSize);
            var size = tagMaker.GetTagSize(tag.Key, fontName, fontSize, imageSize);
            var location = cloudLayouter.PutNextRectangle(size).Location;
            var tagToRender = new TagToRender(location, tag.Key, fontColorHex, fontSize, fontName);

            yield return tagToRender;
        }
    }
}