namespace TagCloudCreation
{
    public class PrepositionRemover : PartOfSpeechPreparer
    {
        public override string PrepareWord(string word, TagCloudCreationOptions options) =>
            ProcessWordByTag(word, (tag, w) => tag == PartOfSpeech.Preposition ? null : w);
    }
}
