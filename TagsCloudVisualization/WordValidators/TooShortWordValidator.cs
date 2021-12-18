namespace TagsCloudVisualization.WordValidators
{
    public class TooShortWordValidator : IWordValidator
    {
        private byte minWordLength;

        public void SetLimit(byte minWordLength)
        {
            this.minWordLength = minWordLength;
        }
        
        public bool Validate(string word)
        {
            return word.Length >= minWordLength;
        }
    }
}