namespace TagsCloudVisualization
{
    public class Tag
    {
        public string Text { get; }
        public int FontSize { get; }
        public int Frequency { get; }
        
        public Tag(string text, int fontSize, int frequency)
        {
            Text = text;
            FontSize = fontSize;
            Frequency = frequency;
        }
    }
}