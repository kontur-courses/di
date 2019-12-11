namespace TagCloud
{
    public class WordToken
    {
        public readonly string Value;
        public readonly int Count;
        public readonly SpeechPartEnum SpeechPart;

        public WordToken(string value, int count, SpeechPartEnum speechPart)
        {
            Value = value;
            Count = count;
            SpeechPart = speechPart;
        }
    }
}
