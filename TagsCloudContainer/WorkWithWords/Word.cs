namespace TagsCloudContainer
{
    public class Word
    {
        public string Value { get; }
        public int Count { get; set; }

        public Word(string value)
        {
            Value = value;
            Count = 1;
        }
    }
}