namespace TagCloud.Data
{
    public class WordInfo
    {
        public readonly string Word;
        public readonly int Occurrences;
        public readonly float Frequency;

        public WordInfo(string word, int occurrences, float frequency)
        {
            Word = word;
            Occurrences = occurrences;
            Frequency = frequency;
        }
    }
}