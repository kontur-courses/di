namespace TagsCloudContainer.WordFilter
{
    public class LengthWordFilter : IFilter
    {
        public LengthWordFilter(int length)
        {
            this.length = length;
        }

        private readonly int length;
        public bool Validate(string word)
        {
            return word.Length > length;
        }
    }
}