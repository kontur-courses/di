namespace TagCloudCreation
{
    public class BasicWordPreparer : IWordPreparer
    {
        public WordInfo PrepareWords(WordInfo stat, TagCloudCreationOptions _)
        {
            return stat.With(w => w.Trim()
                                    .ToLowerInvariant());
        }
    }
}