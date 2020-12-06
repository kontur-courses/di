using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace TagCloud.Infrastructure.Settings.UISettingsManagers
{
    public class LayoutCenterSettingManager : ISettingsManager
    {
        private readonly Func<ISpiralSettingsProvider> settingsProvider;
        private readonly Regex regex;

        public LayoutCenterSettingManager(Func<ISpiralSettingsProvider> settingsProvider)
        {
            this.settingsProvider = settingsProvider;
            regex = new Regex(@"^(?<width>\d+)\s+(?<height>\d+)$");
        }

        public string Title => "Layout Center";
        public string Help => "Choose where you want to see a layout. Point is counting from top left corner";

        public bool TrySet(string value)
        {
            var match = regex.Match(value);
            if (!match.Success)
                return false;
            settingsProvider().Center = new Point(
                int.Parse(match.Groups["width"].Value),
                int.Parse(match.Groups["height"].Value));
            return true;
        }

        public string Get()
        {
            var settings = settingsProvider();
            return $"{settings.Center.X} {settings.Center.Y}";
        }
    }
}