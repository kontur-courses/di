namespace TagsCloudVisualization.WordValidators
{
    public class TooShortWordValidator : IWordValidator
    {
        private readonly int minWordLength;

        public TooShortWordValidator(int minWordLength)
        {
            this.minWordLength = minWordLength;
        }
        
        public bool Validate(string word)
        {
            return word.Length >= minWordLength;
        }
    }
}