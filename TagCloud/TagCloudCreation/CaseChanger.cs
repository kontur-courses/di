using TagCloudVisualization;

namespace TagCloudCreation
{
    public class CaseChanger : IWordPreparer
    {
        /// <inheritdoc cref="IWordPreparer"/>
        public WordInfo PrepareWord(WordInfo stat, TagCloudCreationOptions _)
        {
            var preparedWord = stat.With(w => w.Trim()
                                               .ToLowerInvariant());

            return preparedWord.Word == string.Empty ? null : preparedWord;
        }
    }
}
