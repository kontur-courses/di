namespace TagCloudCreation
{
    public interface IWordPreparer
    {
        /// <summary>
        ///     Transforms word
        /// </summary>
        /// <returns>null if this word should be removed</returns>
        WordInfo PrepareWords(WordInfo stats);
    }

    public class BasicWordPreparer : IWordPreparer
    {
        public WordInfo PrepareWords(WordInfo stats)
        {
            return stats.With(w => w.Trim()
                                    .ToLowerInvariant());
        }
    }
}
