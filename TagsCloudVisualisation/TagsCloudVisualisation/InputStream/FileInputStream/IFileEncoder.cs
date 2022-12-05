namespace TagsCloudVisualisation.InputStream.FileInputStream
{
    public interface IFileEncoder
    {
        string GetText(string fileName);

        bool IsCompatibleFileType(string fileName);

        string GetExpectedFileType();
    }
}