using System;
using System.Text.RegularExpressions;

namespace TagCloud.Infrastructure.Settings.UISettingsManagers
{
    public class ImageSizeSettingsManager : ISettingsManager
    {
        private readonly Func<IImageSettingsProvider> imageSettingsProvider;
        private readonly Regex regex;

        public ImageSizeSettingsManager(Func<IImageSettingsProvider> imageSettingsProvider)
        {
            this.imageSettingsProvider = imageSettingsProvider;
            regex = new Regex(@"^(?<width>\d+)\s+(?<height>\d+)$");
        }

        public string Title => "Size";
        public string Help => "Input image width and height separated by space";

        public bool TrySet(string value)
        {
            var match = regex.Match(value);
            if (!match.Success)
                return false;
            imageSettingsProvider().Width = int.Parse(match.Groups["width"].Value);
            imageSettingsProvider().Height = int.Parse(match.Groups["height"].Value);
            return true;
        }

        public string Get()
        {
            var settings = imageSettingsProvider();
            return $"{settings.Width} {settings.Height}";
        }
    }
}