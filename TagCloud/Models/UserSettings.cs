namespace TagCloud.Models
{
    public class UserSettings
    {
        public UserSettings(ImageSettings imageSettings, string pathToRead)
        {
            ImageSettings = imageSettings;
            PathToRead = pathToRead;
        }

        public ImageSettings ImageSettings { get; }
        public string PathToRead { get; }
    }
}