namespace TagsCloudPainter.FileReader;

public interface IFormatFileReader<TFormat>
{
    public TFormat ReadFile(string path);
}