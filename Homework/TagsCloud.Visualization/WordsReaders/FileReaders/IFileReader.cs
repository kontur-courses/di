namespace TagsCloud.Visualization.WordsReaders.FileReaders
{
    public interface IFileReader
    {
        string Read(string filename);
        bool CanRead(string extension);
    }
}