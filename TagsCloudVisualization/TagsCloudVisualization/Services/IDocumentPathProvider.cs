namespace TagsCloudVisualization
{
    public interface IDocumentPathProvider
    {
        bool TryGetPath(out string path);

        void SetPath(string path);
    }
}