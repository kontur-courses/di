using System;

namespace TagCloud.Infrastructure.Settings.UISettingsManagers
{
    public class ImagePathSettingManager : ISettingsManager
    {
        private readonly Func<IImageSettingsProvider> imageSettingsProvider;

        public ImagePathSettingManager(Func<IImageSettingsProvider> imageSettingsProvider)
        {
            this.imageSettingsProvider = imageSettingsProvider;
        }

        public string Title => "Image path";
        public string Help => "Type image file location to save";

        public bool TrySet(string value)
        {
            imageSettingsProvider().ImagePath = value;
            return true;
        }

        public string Get()
        {
            return imageSettingsProvider().ImagePath;
        }
    }
}