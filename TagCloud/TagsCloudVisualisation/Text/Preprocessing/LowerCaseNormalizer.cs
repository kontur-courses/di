namespace TagsCloudVisualisation.Text.Preprocessing
{
    public class LowerCaseNormalizer : IWordNormalizer
    {
        public string Normalize(string word) => word.ToLower();
    }
}