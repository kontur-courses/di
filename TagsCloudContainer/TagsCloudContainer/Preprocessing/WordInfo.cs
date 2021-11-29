namespace TagsCloudContainer.Preprocessing
{
    public class WordInfo
    {
        public string Word { get; }
        public SpeechPart SpeechPart { get; }

        public WordInfo(string word, SpeechPart speechPart)
        {
            Word = word;
            SpeechPart = speechPart;
        }
    }
}