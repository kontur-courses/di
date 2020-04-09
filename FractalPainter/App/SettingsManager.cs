using System;
using System.Windows.Forms;
using FractalPainting.Infrastructure.Common;

namespace FractalPainting.App
{
    public class SettingsManager
    {
        private readonly IObjectSerializer serializer;
        private readonly IBlobStorage storage;

        private string settingsFilename = "app.settings";

        private static AppSettings DefaultSettings => new AppSettings
        {
            ImagesDirectory = ".",
            ImageSettings = new ImageSettings()
        };

        public SettingsManager(IObjectSerializer serializer, IBlobStorage storage)
        {
            this.serializer = serializer;
            this.storage = storage;
        }

        public AppSettings Load()
        {
            try
            {
                var data = storage.Get(settingsFilename);
                if (data != null)
                {
                    return serializer.Deserialize<AppSettings>(data);
                }

                var defaultSettings = DefaultSettings;
                Save(defaultSettings);

                return defaultSettings;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Не удалось загрузить настройки");
                return DefaultSettings;
            }
        }

        public void Save(AppSettings settings)
        {
            storage.Set(settingsFilename, serializer.Serialize(settings));
        }
    }
}