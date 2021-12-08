namespace TagsCloudVisualization.WordReaders.WordValidators
{
    public class TooShortWordValidator : IWordValidator
    {
        public bool Validate(string word)
        {
            return word.Length >= 4;
        }
    }
}