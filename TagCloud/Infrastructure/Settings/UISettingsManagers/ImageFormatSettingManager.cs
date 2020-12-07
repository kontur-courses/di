using System;
using System.Drawing.Imaging;
using System.Linq;

namespace TagCloud.Infrastructure.Settings.UISettingsManagers
{
    public class ImageFormatSettingManager : ISettingsManager
    {
        private readonly Func<IImageFormatSettingProvider> settingsProvider;

        public ImageFormatSettingManager(Func<IImageFormatSettingProvider> settingsProvider)
        {
            this.settingsProvider = settingsProvider;
        }

        public string Title => "Image Format";
        public string Help => "Choose format: Bmp, Emf, Exif, Gif, Icon, Jpeg, Png, Tiff, Wmf";
        public bool TrySet(string value)
        {
            var propertyInfos = typeof(ImageFormat)
                .GetProperties();
     
            var newFormat = propertyInfos
                .Where(info => info.Name == value)
                .Select(info => info.GetValue(settingsProvider().Format))
                .Cast<ImageFormat>()
                .SingleOrDefault();
            
            if (newFormat == null)
                return false;
            settingsProvider().Format = newFormat;
            return true;
        }

        public string Get()
        {
            return settingsProvider().Format.ToString();
        }
    }
}