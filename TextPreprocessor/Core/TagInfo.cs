namespace TextPreprocessor.Core
{
    public struct TagInfo
    {
        public Tag Tag { get; }
        public int Frequency { get; }

        public TagInfo(Tag tag, int frequency)
        {
            Tag = tag;
            Frequency = frequency;
        }
    }
}