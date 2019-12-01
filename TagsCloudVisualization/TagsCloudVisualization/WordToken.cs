namespace TagsCloudVisualization
{
    public class WordToken
    {
        public readonly string Tag;
        public readonly int TextCount;

        public WordToken(string tag, int textCount)
        {
            Tag = tag;
            TextCount = textCount;
        }
    }
}