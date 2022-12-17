namespace TagsCloudContainer.MorphologicalAnalysis
{
    public readonly struct Word
    {
        public string Text { get; }
        public PartSpeech PartSpeech { get; }

        public Word(string text, PartSpeech partSpeech)
        {
            Text = text;
            PartSpeech = partSpeech;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}