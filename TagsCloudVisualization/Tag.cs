namespace TagsCloudVisualization
{
    public class Tag
    {
        public Tag(float count, string word)
        {
            Count = count;
            Word = word;
        }

        public float Count { get; }
        public string Word { get; }
    }
}