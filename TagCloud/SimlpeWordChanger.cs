namespace TagCloud
{
    public class SimlpeWordChanger : IWordChanger
    {
        public string ChangeWord(string word)
        {
            return word.ToLower();
        }
    }
}