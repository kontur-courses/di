using System.Drawing;
using TagsCloudApp.Settings;

namespace TagsCloudApp
{
    public class BitmapSaver : IBitmapSaver
    {
        private readonly ISaveSettings saveSettings;

        public BitmapSaver(ISaveSettings saveSettings)
        {
            this.saveSettings = saveSettings;
        }

        public void Save(Bitmap bitmap)
        {
            bitmap.Save(saveSettings.OutputFile, saveSettings.ImageFormat);
        }
    }
}