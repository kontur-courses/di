using System;

namespace TagCloud.Infrastructure.Settings.UISettingsManagers
{
    public class SpiralIncrementSettingManager : ISettingsManager
    {
        private readonly Func<ISpiralSettingsProvider> settingProvider;

        public SpiralIncrementSettingManager(Func<ISpiralSettingsProvider> settingProvider)
        {
            this.settingProvider = settingProvider;
        }

        public string Title => "Spiral increment";
        public string Help => "Choose increment to change placing strategy";

        public bool TrySet(string value)
        {
            if (!int.TryParse(value, out var number))
                return false;
            settingProvider().Increment = number;
            return true;
        }

        public string Get()
        {
            return settingProvider().Increment.ToString();
        }
    }
}