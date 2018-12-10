namespace TagsCloudContainer.WordsTransformers
{
    public class WordsTransformerToLowerCase : IWordsTransformer
    {
        public string TransformWord(string word)
            => word.ToLower();
    }
}