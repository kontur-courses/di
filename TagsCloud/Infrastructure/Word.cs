namespace TagsCloud.Infrastructure
{
    public class Word
    {
        public string Value { get; set; }
        public int Weight { get; set; }

        public Word() {}

        public Word(string value, int weight)
        {
            Value = value;
            Weight = weight;
        }
    }
}
