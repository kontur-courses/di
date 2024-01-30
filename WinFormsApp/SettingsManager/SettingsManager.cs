using System.Xml.Serialization
using TagsCloudContainer.SettingsClasses;

namespace WinFormsApp.SettingsManager
{
    public static class SettingsManager
    {
        private const string settingsFile = "settings.bin";

        // TODO: save and load settings here
        public static void SaveSettings(AppSettings settings, string filePath = settingsFile)
        {
            var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            var xmlSerializer = new XmlSerializer(typeof(AppSettings));
            //binaryFormatter.Serialize(fileStream, settings);
            fileStream.Close();
        }

        public static AppSettings LoadSettings(string filePath = settingsFile)
        {
            AppSettings settings = null;
            //try
            //{
            //    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //    var binaryFormatter = new BinaryFormatter();
            //    settings = (AppSettings)binaryFormatter.Deserialize(fileStream);
            //    fileStream.Close();
            //}
            //catch
            //{
            settings = new AppSettings();
            settings.DrawingSettings = new CloudDrawingSettings();
            SaveSettings(settings);
            //}

            return settings;
        }
    }
}
