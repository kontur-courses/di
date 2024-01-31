namespace TagsCloudContainer.Client
{
    public interface ITagCloudClient
    {
        public void DrawImage(string sourceFilePath, string boringFilePath);

        public void SaveImage(string filePath);
    }
}