namespace TagsCloudTextPreparation
{
    public class FrequencyWord
    {
        public string Word { get; }
        public int Count { get; }

        public FrequencyWord(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}