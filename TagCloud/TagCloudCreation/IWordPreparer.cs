using TagCloudVisualization;

namespace TagCloudCreation
{
    public interface IWordPreparer
    {
        /// <summary>
        ///     Transforms word
        /// </summary>
        /// <returns>null if this word should be removed</returns>
        WordInfo PrepareWord(WordInfo stat, TagCloudCreationOptions options);
    }
}
