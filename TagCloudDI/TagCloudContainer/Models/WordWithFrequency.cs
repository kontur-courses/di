namespace TagCloudContainer.Models
{
    public class TagWithFrequency
    {
        public string Word { get; }
        public int Count { get; }
        public TagWithFrequency(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}
