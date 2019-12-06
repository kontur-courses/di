using System.Drawing;
using TagsCloudContainer.Vizualization;

namespace TagsCloudContainer.Settings_Providing
{
    public class SettingsProvider : ISettingsProvider
    {
        public Settings GetSettings()
        {
            return GetDefaultSettings();
        }

        private Settings GetDefaultSettings()
        {
            return new Settings("", "",
                new ColoringOptions(new SolidBrush(Color.Transparent),
                    new SolidBrush(Color.White), new Pen(Color.Transparent), new SolidBrush(Color.Blue)));
        }
    }
}

