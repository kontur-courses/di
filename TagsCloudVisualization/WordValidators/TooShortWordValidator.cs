namespace TagsCloudVisualization.WordValidators
{
    public class TooShortWordValidator : IWordValidator
    {
        private int minWordLength;

        public void SetLimit(int minWordLength)
        {
            this.minWordLength = minWordLength;
        }
        
        public bool Validate(string word)
        {
            return word.Length >= minWordLength;
        }
    }
}