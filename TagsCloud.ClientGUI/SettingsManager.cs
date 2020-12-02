using System;
using System.IO;
using System.Windows.Forms;
using TagsCloud.ClientGUI.Infrastructure;

namespace TagsCloud.ClientGUI
{
    public class SettingsManager
    {
        private readonly IObjectSerializer serializer;
        private readonly IBlobStorage storage;
        private string settingsFileName;

        public SettingsManager(IObjectSerializer serializer, IBlobStorage storage)
        {
            this.serializer = serializer;
            this.storage = storage;
        }

        public AppSettings Load()
        {
            try
            {
                settingsFileName = "../../../app.settings";
                var data = storage.Get(settingsFileName);
                if (data == null)
                    return CreateDefaultSettings();
                return serializer.Deserialize<AppSettings>(data);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "не удалось загрузить настройки");
                return CreateDefaultSettings();
            }
        }

        private static AppSettings CreateDefaultSettings()
        {
            return new AppSettings
            {
                ImagesDirectory = ".",
                ImageSettings = new ImageSettings()
            };
        }
    }
}
