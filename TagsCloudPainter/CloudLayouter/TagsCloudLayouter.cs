using System.Drawing;
using TagsCloudPainter.FormPointer;
using TagsCloudPainter.Settings;
using TagsCloudPainter.Tags;

namespace TagsCloudPainter.CloudLayouter;

public class TagsCloudLayouter : ICloudLayouter
{
    private readonly Lazy<CloudSettings> cloudSettings;
    private readonly IFormPointer formPointer;
    private readonly TagSettings tagSettings;
    private TagsCloud cloud;

    public TagsCloudLayouter(Lazy<CloudSettings> cloudSettings, IFormPointer formPointer, TagSettings tagSettings)
    {
        this.cloudSettings = cloudSettings ?? throw new ArgumentNullException(nameof(cloudSettings));
        this.formPointer = formPointer ?? throw new ArgumentNullException(nameof(formPointer));
        this.tagSettings = tagSettings ?? throw new ArgumentNullException(nameof(tagSettings));
    }

    private TagsCloud Cloud
    {
        get => cloud ??= new TagsCloud(cloudSettings.Value.CloudCenter, []);
        set => cloud = value;
    }

    public Rectangle PutNextTag(Tag tag)
    {
        var rectangleSize = Utils.Utils.GetStringSize(tag.Value, tagSettings.TagFontName, tag.FontSize);
        if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
            throw new ArgumentException("either width or height of rectangle size is not possitive");

        var nextRectangle = Utils.Utils.GetRectangleFromCenter(formPointer.GetNextPoint(), rectangleSize);
        while (Cloud.Tags.Values.Any(rectangle => rectangle.IntersectsWith(nextRectangle)))
            nextRectangle = Utils.Utils.GetRectangleFromCenter(formPointer.GetNextPoint(), rectangleSize);

        Cloud.AddTag(tag, nextRectangle);

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
        return new TagsCloud(Cloud.Center, Cloud.Tags);
    }

    public void Reset()
    {
        formPointer.Reset();
        Cloud = new TagsCloud(cloudSettings.Value.CloudCenter, []);
    }
}