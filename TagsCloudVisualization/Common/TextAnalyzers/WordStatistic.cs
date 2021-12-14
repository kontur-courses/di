namespace TagsCloudVisualization.Common.TextAnalyzers
{
    public class WordStatistic
    {
        public string Text { get; }
        public int Count { get; }

        public WordStatistic(string text, int count)
        {
            Text = text;
            Count = count;
        }
    }
}