namespace TagCloud.App.WordPreprocessorDriver.InputStream;

public class FromFileStreamContext
{
    public readonly string Filename;
    public readonly IFileEncoder FileEncoder;
    
    public FromFileStreamContext(string filename, IFileEncoder fileEncoder)
    {
        Filename = filename;
        FileEncoder = fileEncoder;
    }
}