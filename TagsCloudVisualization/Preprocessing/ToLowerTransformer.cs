namespace TagsCloudVisualization.Preprocessing
{
    public class ToLowerTransformer : IWordTransformer
    {
        public string TransformWord(string word)
        {
            return word.ToLower();
        }
    }
}
