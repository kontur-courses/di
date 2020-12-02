using TagsCloud.ClientGUI.Infrastructure;

namespace TagsCloud.ClientGUI
{
    public class SettingsManager
    {
        public AppSettings Load()
        {
            return new AppSettings
            {
                ImageSettings = new ImageSettings()
            };
        }
    }
}