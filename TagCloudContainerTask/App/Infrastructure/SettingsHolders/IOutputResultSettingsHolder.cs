using System.Drawing.Imaging;

namespace App.Infrastructure.SettingsHolders
{
    public interface IOutputResultSettingsHolder
    {
        string OutputFilePath { get; }
        ImageFormat ImageFormat { get; }
    }
}