namespace TagsCloudContainer
{
    public interface IWordCloudSaver
    {
        public string SaveCloud(string name, ImageSettings imageSettings);
    }
}