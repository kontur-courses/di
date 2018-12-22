namespace TagsCloudContainer.Infrastructure.FileManaging
{
    public interface IFileManager
    {
        string MakeFile();
        void WriteTextInFile(string fileName, string text);
        string ReadTextFromFile(string fileName);
        void DeleteAllFiles();
    }
}