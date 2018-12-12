namespace TagCloud.Core.TextWorking.WordsReading.WordsReadersForFiles
{
    public interface IWordsReaderForFile : IWordsReader
    {
        /// <summary>
        /// <returns>Extension of files allowed by this reader (including dot)</returns>
        /// </summary>
        /// <remarks>
        /// Dot is needed here to differ ".abc_x" and ".x"
        /// </remarks>
        string ReadingFileExtension { get; }
    }
}