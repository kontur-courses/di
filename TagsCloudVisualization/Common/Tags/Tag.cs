namespace TagsCloudVisualization.Common.Tags
{
    public class Tag
    {
        public string Text { get; }
        public float Weight { get; }

        public Tag(string text, float weight)
        {
            Text = text;
            Weight = weight;
        }
    }
}