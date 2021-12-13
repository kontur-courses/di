namespace TagsCloudVisualization
{
    public class Token
    {
        public readonly string Value;
        public readonly double Weight;

        public Token(string value, double weight)
        {
            Value = value;
            Weight = weight;
        }
    }
}