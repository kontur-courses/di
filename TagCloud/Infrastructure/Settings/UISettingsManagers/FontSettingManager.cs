using System;
using System.Drawing;

namespace TagCloud.Infrastructure.Settings.UISettingsManagers
{
    public class FontSettingManager : ISettingsManager
    {
        private readonly Func<IFontSettingProvider> settingProvider;

        public FontSettingManager(Func<IFontSettingProvider> settingProvider)
        {
            this.settingProvider = settingProvider;
        }

        public string Title => "Font";
        public string Help => "Choose font family name to write tags";

        public bool TrySet(string value)
        {
            try
            {
                settingProvider().FontFamily = new FontFamily(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public string Get()
        {
            return settingProvider().FontFamily.Name;
        }
    }
}