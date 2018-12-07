namespace TagCloud
{
    public class SimpleWordChanger : IWordChanger
    {
        public string ChangeWord(string word)
        {
            return word.ToLower();
        }
    }
}