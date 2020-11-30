using System.Diagnostics.Tracing;

namespace TagsCloudContainer
{
    public class WordValidator : IWordValidator
    {
        public bool IsValidWord(string word)
        {
            return word.Length > 2;
        }
    }
}