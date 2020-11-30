using System;
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
                settingsFileName = "app.settings";
                var data = storage.Get(settingsFileName);
                if (data == null)
                {
                    var defaultSettings = CreateDefaultSettings();
                    Save(defaultSettings);
                    return defaultSettings;
                }
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

        public void Save(AppSettings settings)
        {
            storage.Set(settingsFileName, serializer.Serialize(settings));
        }
    }
}
