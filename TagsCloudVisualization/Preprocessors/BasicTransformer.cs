namespace TagsCloudVisualization.Preprocessors
{
    public class BasicTransformer : IWordTransformer
    {
        public string TransformWord(string word)
        {
            return word.ToLower();
        }
    }
}
