namespace TagCloud.Abstractions;

public interface ICloudCreator
{
    IEnumerable<IDrawableTag> CreateTagCloud(IEnumerable<ITag> tags);
}