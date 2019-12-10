namespace TagsCloudContainer.TokensAndSettings
{
    public class ProcessedWord
    {
        public string Word { get; }
        public string PartOfSpeech { get; }

        public ProcessedWord(string word, string partOfSpeech)
        {
            Word = word;
            PartOfSpeech = partOfSpeech;
        }
    }
}
