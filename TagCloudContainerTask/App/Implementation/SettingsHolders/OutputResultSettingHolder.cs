using System.Drawing.Imaging;
using App.Infrastructure.SettingsHolders;

namespace App.Implementation.SettingsHolders
{
    public class OutputResultSettingHolder : IOutputResultSettingsHolder
    {
        public string OutputFilePath { get; set; }

        public ImageFormat ImageFormat { get; set; }
    }
}