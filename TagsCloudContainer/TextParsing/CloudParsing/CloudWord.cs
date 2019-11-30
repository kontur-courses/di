namespace TagsCloudContainer.TextParsing.CloudParsing
{
    public class CloudWord
    {
        public string Word { get; }
        public int Count { get; set; }

        public CloudWord(string word)
        {
            Word = word;
            Count = 0;
        }

        public void AddCount()
        {
            Count += 1;
        }
    }
}