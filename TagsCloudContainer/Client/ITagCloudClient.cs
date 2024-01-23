namespace TagsCloudContainer.Client
{
    public interface ITagCloudClient
    {
        public void SetSettings<TSettings>(TSettings property);

        public string SetBoringFilePath(string filePath);

        public string SetSourceFilePath(string filePath);

        public void DrawImage(string sourceFilePath, string boringFilePath, int imgWidth, int imgHeight);

        public void SaveImage(string filePath);
    }
}