namespace TagCloudCreation
{
    public class CaseChanger : IWordPreparer
    {
        /// <inheritdoc cref="IWordPreparer" />
        public string PrepareWord(string word, TagCloudCreationOptions _)
        {
            var preparedWord = word.ToLowerInvariant();

            return word == string.Empty ? null : preparedWord;
        }
    }
}
