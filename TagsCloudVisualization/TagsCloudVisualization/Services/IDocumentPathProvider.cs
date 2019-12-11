namespace TagsCloudVisualization.Services
{
    public interface IDocumentPathProvider
    {
        bool TryGetPath(out string path);
        void SetPath(string path);
    }
}