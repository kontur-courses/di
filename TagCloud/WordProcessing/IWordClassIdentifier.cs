namespace TagCloud.WordProcessing
{
    public interface IWordClassIdentifier
    {
        WordClass GetWordClass(string word);
    }
}
