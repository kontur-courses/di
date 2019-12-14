namespace TagCloud.WordsPreprocessing
{
    public class Word
    {
        public string Value { get; }
        public SpeechPart PartOfSpeech { get; }
        public int Count { get; set; }
        public double Frequency { get; set; }


        public Word(string value, SpeechPart speechPart)
        {
            Value = value;
            PartOfSpeech = speechPart;
        }
    }
}
