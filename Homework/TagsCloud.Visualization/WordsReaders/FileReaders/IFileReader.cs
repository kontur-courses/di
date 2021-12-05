namespace TagsCloud.Visualization.WordsReaders.FileReaders
{
    public interface IFileReader
    {
        string Extension { get; }
        string Read(string filename);
    }
}