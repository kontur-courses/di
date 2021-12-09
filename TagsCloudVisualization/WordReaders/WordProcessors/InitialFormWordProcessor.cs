using WeCantSpell.Hunspell;

namespace TagsCloudVisualization.WordReaders.WordProcessors
{
    public class InitialFormWordProcessor : IWordProcessor
    {
        public string ProcessWord(string word)
        {
            return word;
        }
    }
}