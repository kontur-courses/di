namespace TagsCloudVisualization.Common.Stemers
{
    public interface IStemer
    {
        public bool TryGetStem(string word, out string stem);
    }
}