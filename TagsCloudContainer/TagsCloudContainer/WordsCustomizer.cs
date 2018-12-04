namespace TagsCloudContainer
{
    internal class WordsCustomizer
    {
        public string CustomizeWord(string word)
        {
            if (IgnoreWord(word))
                return null;

            word = word.ToLower();
            return ToBaseForm(word);
        }

        public bool IgnoreWord(string word)
        {
            return false;
        }

        private string ToBaseForm(string word)
        {
            return word;
        }
    }
}
