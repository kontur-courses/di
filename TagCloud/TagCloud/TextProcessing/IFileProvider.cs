namespace TagCloud.TextProcessing
{
    public interface IFileProvider
    {
        string GetTxtFilePath(string path);
    }
}