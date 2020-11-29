namespace TagCloud.TextConverters
{
    internal interface IWordExcluder
    {
        public bool MustBeExclude(string word);
    }
}
