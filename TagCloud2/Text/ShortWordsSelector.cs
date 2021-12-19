namespace TagCloud2.Text
{
    public class ShortWordsSelector : ISillyWordSelector
    {
        public bool IsWordSilly(string word)
        {
            return word.Length <= 3;
        }
    }
}
