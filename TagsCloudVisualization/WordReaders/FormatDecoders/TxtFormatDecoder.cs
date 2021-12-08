namespace TagsCloudVisualization.WordReaders.FormatDecoders
{
    public class TxtFormatDecoder : IFormatDecoder
    {
        public string FormatExtension => ".txt";
        public string Decode(string text) => text;
    }
}