namespace TagsCloudVisualization.Structures
{
    public class WordInfo
    {
        public string StandartForm { get; private set; }
        public string PartOfSpeech { get; private set; }

        public WordInfo(string standartForm, string partOfSpeech)
        {
            StandartForm = standartForm;
            PartOfSpeech = partOfSpeech;
        }
    }
}
