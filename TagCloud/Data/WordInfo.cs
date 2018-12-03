namespace TagCloud.Data
{
    public class WordInfo
    {
        public readonly string Word;
        public readonly int Occurrences;

        public WordInfo(string word, int occurrences)
        {
            Word = word;
            Occurrences = occurrences;
        }
    }
}