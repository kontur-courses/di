namespace TagCloud.WordConverter
{
    public class ToLowerConverter : IWordConverter
    {
        public string Convert(string word)
        {
            return word.ToLower();
        }
    }
}
