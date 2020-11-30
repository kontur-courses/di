namespace TagsCloudVisualisation.Text.Preprocessing
{
    public class WordLengthFilter : IWordFilter
    {
        private readonly int minWordLength;

        public WordLengthFilter(int minWordLength)
        {
            this.minWordLength = minWordLength;
        }

        public bool IsValidWord(string word) => word.Length >= minWordLength;
    }
}