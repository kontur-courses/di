namespace TagsCloud.TextProcessing
{
    public class WordInfo
    {
        public string Value { get; private set; }
        public int Frequence { get; private set; }

        public WordInfo(string value, int frequence)
        {
            Value = value;
            Frequence = frequence;
        }
    }
}
