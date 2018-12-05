namespace TagsCloudVisualization
{
    public class FrequentedWord
    {
        public string Word { get; }
        public int Frequency { get; set; }

        public FrequentedWord(string word)
        {
            Word = word;
        }
    }
}