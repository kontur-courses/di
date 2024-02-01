using System.Drawing;
using TagsCloudPainter.Extensions;
using TagsCloudPainter.FormPointer;
using TagsCloudPainter.Settings.Cloud;
using TagsCloudPainter.Settings.Tag;
using TagsCloudPainter.Sizer;
using TagsCloudPainter.Tags;

namespace TagsCloudPainter.CloudLayouter;

public class TagsCloudLayouter : ICloudLayouter
{
    private readonly ICloudSettings cloudSettings;
    private readonly IFormPointer formPointer;
    private readonly IStringSizer stringSizer;
    private readonly ITagSettings tagSettings;
    private TagsCloud cloud;

    public TagsCloudLayouter(
        ICloudSettings cloudSettings,
        IFormPointer formPointer,
        ITagSettings tagSettings,
        IStringSizer stringSizer)
    {
        this.cloudSettings = cloudSettings ?? throw new ArgumentNullException(nameof(cloudSettings));
        this.formPointer = formPointer ?? throw new ArgumentNullException(nameof(formPointer));
        this.tagSettings = tagSettings ?? throw new ArgumentNullException(nameof(tagSettings));
        this.stringSizer = stringSizer ?? throw new ArgumentNullException();
        cloud = new TagsCloud(cloudSettings.CloudCenter, []);
    }

    public Rectangle PutNextTag(Tag tag)
    {
        var tagSize = stringSizer.GetStringSize(tag.Value, tagSettings.TagFontName, tag.FontSize);
        if (tagSize.Height <= 0 || tagSize.Width <= 0)
            throw new ArgumentException("either width or height of rectangle size is not possitive");

        var nextRectangle = formPointer.GetNextPoint().GetRectangle(tagSize);
        while (cloud.Tags.Any(pair => pair.Item2.IntersectsWith(nextRectangle)))
            nextRectangle = formPointer.GetNextPoint().GetRectangle(tagSize);

        cloud.AddTag(tag, nextRectangle);

        return nextRectangle;
    }

    public void PutTags(List<Tag> tags)
    {
        if (tags.Count == 0)
            throw new ArgumentException("пустые размеры");
        foreach (var tag in tags)
            PutNextTag(tag);
    }

    public TagsCloud GetCloud()
    {
        return new TagsCloud(cloud.Center, cloud.Tags);
    }

    public void Reset()
    {
        formPointer.Reset();
        cloud = new TagsCloud(cloudSettings.CloudCenter, []);
    }
}