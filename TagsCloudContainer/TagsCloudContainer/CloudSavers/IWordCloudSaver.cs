namespace TagsCloudContainer
{
    public interface IWordCloudSaver
    {
        public string SaveCloud(string pathToSaveDir, string name, ImageSettings imageSettings, ImageFormats imageFormat);
    }
}