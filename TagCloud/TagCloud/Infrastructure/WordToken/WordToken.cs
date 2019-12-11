namespace TagCloud
{
    public class WordToken
    {
        public readonly string Value;
        public readonly int Count;
        public readonly SpeechPart SpeechPart;

        public WordToken(string value, int count, SpeechPart speechPart)
        {
            Value = value;
            Count = count;
            SpeechPart = speechPart;
        }
    }
}
