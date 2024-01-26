using System.Drawing;
using TagsCloudPainter.FormPointer;
using TagsCloudPainter.Settings;
using TagsCloudPainter.Tags;

namespace TagsCloudPainter.CloudLayouter;

public class TagsCloudLayouter : ICloudLayouter
{
    private readonly IFormPointer formPointer = null!;
    private readonly TagSettings tagSettings;
    private readonly Lazy<CloudSettings> cloudSettings;
    private TagsCloud cloud;
    private TagsCloud Cloud 
    {
        get =>  cloud ??= new TagsCloud(cloudSettings.Value.CloudCenter, []);
    }

    public TagsCloudLayouter(Lazy<CloudSettings> cloudSettings, IFormPointer formPointer, TagSettings tagSettings)
    {
        this.cloudSettings = cloudSettings;
        this.formPointer = null;
        this.tagSettings = tagSettings;
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
    }
}