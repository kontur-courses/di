using System.Drawing;
using TagCloud.Common.Options;

namespace TagCloud.Common.Saver;

public class LocalCloudSaver : ICloudSaver
{
    public SavingOptions CloudSavingOptions { get; }
    public LocalCloudSaver(SavingOptions savingOptions)
    {
        CloudSavingOptions = savingOptions;
    }

    public void SaveCloud(Bitmap bmp)
    {
        if (!Directory.Exists(CloudSavingOptions.SavePath))
        {
            Directory.CreateDirectory(CloudSavingOptions.SavePath);
        }

        var path = Path.Combine(CloudSavingOptions.SavePath, CloudSavingOptions.FileName + "." + CloudSavingOptions.SavingImageFormat.ToString().ToLower());
        bmp.Save(path);
        bmp.Dispose();
    }
}