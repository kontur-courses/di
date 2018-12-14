namespace TagCloud.Reader.FormatReader
{
    public interface IFormatReader
    {
        string Format { get; }

        string Read(string fileName);
    }
}