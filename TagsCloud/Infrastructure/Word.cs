namespace TagsCloud.Infrastructure
{
    public class Word
    {
        public Word()
        {
        }

        public Word(string value, int weight)
        {
            Value = value;
            Weight = weight;
        }

        public string Value { get; set; }
        public int Weight { get; set; }
    }
}