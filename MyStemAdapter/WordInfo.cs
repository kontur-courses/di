namespace MyStemAdapter
{
    public struct WordInfo
    {
        public string Word { get; }
        public PartOfSpeech PartOfSpeech { get; }
        public string Stem { get; }

        public WordInfo(string word, PartOfSpeech partOfSpeech, string stem)
        {
            Word = word;
            PartOfSpeech = partOfSpeech;
            Stem = stem;
        }
    }
}
