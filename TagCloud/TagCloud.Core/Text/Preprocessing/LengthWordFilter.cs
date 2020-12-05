namespace TagCloud.Core.Text.Preprocessing
{
    public class LengthWordFilter : IWordFilter
    {
        public bool IsValidWord(string word) => word.Length >= 3;
    }
}