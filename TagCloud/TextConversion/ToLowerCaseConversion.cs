namespace TagCloud.TextConversion
{
    public class ToLowerCaseConversion : ITextConversion
    {
        public string ConvertWord(string word)
        {
            return word.MakeLettersLowerCase();
        }
    }
}