namespace TagsCloud.TextProcessing
{
    public class WordInfo
    {
        public string Word { get; private set; }
        public int Frequence { get; private set; }

        public WordInfo(string value, int frequence)
        {
            Word = value;
            Frequence = frequence;
        }
    }
}
