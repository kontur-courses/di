namespace TagsCloudContainer
{
    public class ToLowerCaseFormatter : IWordsFormatter
    {
        public string Format(string word)
        {
            return word.ToLower();
        }
    }
}