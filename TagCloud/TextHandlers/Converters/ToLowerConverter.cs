namespace TagCloud.TextHandlers.Converters
{
    public class ToLowerConverter : IConverter
    {
        public string Convert(string word)
        {
            return word.ToLower();
        }
    }
}