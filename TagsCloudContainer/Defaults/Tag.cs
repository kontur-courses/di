using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults;

public class Tag : ITag
{
    public Tag(string value, double relativeSize)
    {
        Value = value;
        RelativeSize = relativeSize;
    }

    public string Value { get; }

    public double RelativeSize { get; }
}