namespace TagsCloudVisualization.Common.WordFilters
{
    public interface ICustomWordFilter : IWordFilter
    {
        public void Load(string path);
    }
}