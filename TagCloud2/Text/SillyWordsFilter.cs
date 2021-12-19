namespace TagCloud2.Text
{
    public class SillyWordsFilter : ISillyWordsFilter
    {
        public string FilterSillyWords(string input, ISillyWordSelector selector)
        {
            if (selector.IsWordSilly(input))
            {
                return "";
            }
            return input;
        }
    }
}
