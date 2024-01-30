using System.Runtime.Serialization.Formatters.Binary;
using TagsCloudContainer.SettingsClasses;

namespace WinFormsApp.SettingsManager
{
    public static class SettingsManager
    {

        // TODO: save and load settings here
        public static void SaveSettings(AppSettings settings, string filePath = "settings.bin")
        {
            //var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            //var binaryFormatter = new BinaryFormatter();
            //binaryFormatter.Serialize(fileStream, settings);
            //fileStream.Close();
        }

        public static AppSettings LoadSettings(string filePath = "settings.bin")
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
