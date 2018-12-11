namespace TagCloudCreation
{
    public class PrepositionRemover : PosRemover
    {
        public override string PrepareWord(string word, TagCloudCreationOptions options) =>
            PrepareWord(word, w => w.EndsWith("IN "));
    }
}
