namespace TagCloud.TextConverters
{
    internal class WordsExcluder : IWordExcluder
    {
        public bool MustBeExclude(string word) => false;
    }
}
