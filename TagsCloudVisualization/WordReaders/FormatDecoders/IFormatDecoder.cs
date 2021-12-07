namespace TagsCloudVisualization.WordReaders.FormatDecoders
{
    public interface IFormatDecoder
    {
        string FormatExtension { get; }
        string Decode(string text);
    }
}