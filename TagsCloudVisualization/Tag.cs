namespace TagsCloudVisualization
{
    public class Tag
    { 
        public float Count { get; }
        public string Word { get; }
        public Tag(float count, string word)
        {
            Count = count;
            Word = word;
        }
    }
}