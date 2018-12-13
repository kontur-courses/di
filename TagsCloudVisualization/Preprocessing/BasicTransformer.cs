namespace TagsCloudVisualization.Preprocessing
{
    public class BasicTransformer : IWordTransformer
    {
        public string TransformWord(string word)
        {
            return word.ToLower();
        }
    }
}
