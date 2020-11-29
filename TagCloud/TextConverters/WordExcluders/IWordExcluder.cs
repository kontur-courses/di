namespace TagCloud.TextConverters.WordExcluders
{
    internal interface IWordExcluder
    {
        public bool MustBeExclude(string word);
    }
}
