using System.Drawing.Imaging;
using App.Infrastructure.SettingsHolders;

namespace App.Implementation.SettingsHolders
{
    public class OutputResultSettings : IOutputResultSettingsHolder
    {
        public string OutputFilePath { get; set; } = "layout.png";

        public ImageFormat ImageFormat { get; set; } = ImageFormat.Png;
    }
}