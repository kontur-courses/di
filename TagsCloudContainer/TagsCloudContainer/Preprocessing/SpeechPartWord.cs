namespace TagsCloudContainer.Preprocessing
{
    public class SpeechPartWord
    {
        public string Word { get; }
        public SpeechPart SpeechPart { get; }

        public SpeechPartWord(string word, SpeechPart speechPart)
        {
            Word = word;
            SpeechPart = speechPart;
        }
    }
}