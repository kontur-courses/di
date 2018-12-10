namespace TagCloud.Core.TextWorking.WordsReading.WordsReadersForFiles
{
    public interface IWordsReaderForFile : IWordsReader
    {
        string ReadingFileExtension { get; }
    }
}