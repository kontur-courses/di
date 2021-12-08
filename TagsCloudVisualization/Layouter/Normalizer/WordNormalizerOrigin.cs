
namespace TagsCloudVisualization.Layouter.Normalizer
{
    public class WordNormalizerOrigin : IWordNormalizer
    {
        public Word NormalizeWord(Word word)
        {
            word.WordText = word.WordText.ToLower();
            return word;
        }
    }
}
