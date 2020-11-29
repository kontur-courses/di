namespace TagCloud
{
    internal interface IWordExcluder
    {
        public bool MustBeExclude(string word);
    }
}
