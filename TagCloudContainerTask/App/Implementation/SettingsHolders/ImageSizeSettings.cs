using System.Drawing;
using App.Infrastructure.SettingsHolders;

namespace App.Implementation.SettingsHolders
{
    public class ImageSizeSettings : IImageSizeSettingsHolder
    {
        public Size Size { get; set; }
    }
}