namespace TagCloud
{
    public class TagInfo
    {
        public readonly string Value;
        public readonly double Proportion;
        
        public TagInfo(string value, double proportion)
        {
            Value = value;
            Proportion = proportion;
        }
    }
}