namespace TagsCloudVisualization.Settings
{
    public class SaverSetting
    {
        public string Directory { get; }
        public string ImageName { get; }

        public SaverSetting(string directory, string imageName)
        {
            Directory = directory;
            ImageName = imageName;
        }
    }
}