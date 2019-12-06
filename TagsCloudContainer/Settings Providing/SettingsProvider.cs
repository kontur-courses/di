using System.Drawing;

namespace TagsCloudContainer.Parameters_Providing
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
                    new SolidBrush(Color.White), new Pen(Color.Black), new SolidBrush(Color.Blue)));
        }
    }
}

