namespace TagsCloudContainer.WordsPreparator
{
    public class WordInfo
    {
        public WordInfo(string lexeme, SpeechPart speechPart)
        {
            Lemma = lexeme;
            SpeechPart = speechPart;
        }

        public string Lemma { get; }
        public SpeechPart SpeechPart { get; }
    }
}