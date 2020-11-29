namespace TagCloud.TextConverters.WordExcluders
{
    internal class WordsExcluder : IWordExcluder
    {
        public bool MustBeExclude(string word) => false;
    }
}
