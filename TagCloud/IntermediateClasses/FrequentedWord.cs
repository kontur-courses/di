namespace TagCloud.IntermediateClasses
{
    public class FrequentedWord
    {
        public FrequentedWord(string word, int frequency)
        {
            Word = word;
            Frequency = frequency;
        }

        public string Word { get; }
        public int Frequency { get; set; }
    }
}