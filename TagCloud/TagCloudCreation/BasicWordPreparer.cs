namespace TagCloudCreation
{
    public class BasicWordPreparer : IWordPreparer
    {
        public WordInfo PrepareWord(WordInfo stat, TagCloudCreationOptions _)
        {
            return stat.With(w => w.Trim()
                                    .ToLowerInvariant());
        }
    }
}