using System.Drawing;
using TagsCloudPainter.FormPointer;
using TagsCloudPainter.Settings;
using TagsCloudPainter.Tags;

namespace TagsCloudPainter.CloudLayouter;

public class TagsCloudLayouter : ICloudLayouter
{
    private readonly IFormPointer formPointer;
    private readonly TagSettings tagSettings;
    private TagsCloud cloud;
    private readonly CloudSettings cloudSettings;

    public TagsCloudLayouter(CloudSettings cloudSettings, IFormPointer formPointer, TagSettings tagSettings)
    {
        this.cloudSettings = cloudSettings;
        this.formPointer = formPointer;
        this.tagSettings = tagSettings;
    }

    public Rectangle PutNextTag(Tag tag)
    {
        FailIfCloudNotInitialized();

        var rectangleSize = Utils.Utils.GetStringSize(tag.Value, tagSettings.TagFontName, tag.FontSize);
        if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
            throw new ArgumentException("either width or height of rectangle size is not possitive");

        var nextRectangle = Utils.Utils.GetRectangleFromCenter(formPointer.GetNextPoint(), rectangleSize);
        while (cloud.Tags.Values.Any(rectangle => rectangle.IntersectsWith(nextRectangle)))
            nextRectangle = Utils.Utils.GetRectangleFromCenter(formPointer.GetNextPoint(), rectangleSize);

        cloud.AddTag(tag, nextRectangle);

        return nextRectangle;
    }

    public void PutTags(List<Tag> tags)
    {
        FailIfCloudNotInitialized();

        if (tags.Count == 0)
            throw new ArgumentException("пустые размеры");
        foreach (var tag in tags)
            PutNextTag(tag);
    }

    public TagsCloud GetCloud()
    {
        return new TagsCloud(cloud.Center, cloud.Tags);
    }

    public void InitializeCloud()
    {
        cloud = new TagsCloud(cloudSettings.CloudCenter, []);
    }

    public void Reset()
    {
        formPointer.Reset();
    }

    public void FailIfCloudNotInitialized()
    {
        if (cloud is null)
            throw new InvalidOperationException("Initialize cloud before other method call!");
    }
}