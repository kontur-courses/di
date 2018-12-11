namespace TagCloudCreation
{
    public interface IWordPreparer
    {
        /// <summary>
        ///     Transforms word
        /// </summary>
        /// <returns>null if this word should be removed</returns>
        string PrepareWord(string word, TagCloudCreationOptions options);
    }
}
