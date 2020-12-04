namespace TagsCloudVisualisation.Text.Preprocessing
{
    [VisibleName("Only with length more or equal to 3")]
    public class LengthWordFilter : IWordFilter
    {
        public bool IsValidWord(string word) => word.Length >= 3;
    }
}