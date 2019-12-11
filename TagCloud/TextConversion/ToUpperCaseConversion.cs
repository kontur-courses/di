namespace TagCloud.TextConversion
{
    public class ToUpperCaseConversion : ITextConversion
    {
        public string ConvertWord(string word)
        {
            return word.MakeLettersUpperCase();
        }
    }
}