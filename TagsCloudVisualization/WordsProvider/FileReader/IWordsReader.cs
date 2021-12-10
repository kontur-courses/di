namespace TagsCloudVisualization.WordsProvider.FileReader
{
    public interface IWordsReader
    {
        bool IsSupportedFileExtension(string extension);
        string GetFileContent(string path);
    }
}