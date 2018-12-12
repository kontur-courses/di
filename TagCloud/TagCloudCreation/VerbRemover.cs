using System.Linq;

namespace TagCloudCreation
{
    public class VerbRemover : PartOfSpeechPreparer
    {
        public override string PrepareWord(string word, TagCloudCreationOptions options) =>
            ProcessWordByTag(word, (tag, w) => tag == PartOfSpeech.Verb ? null : w);
    }
}
