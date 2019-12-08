namespace TagsCloudContainer.FileManager
{
    public interface IFileManager
    {
        string ReadFile(string inputFile);

        string MakeFile();

        void WriteInFile(string fileName, string text);
    }
}