using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FileInteractions.Savers
{
    public class BitmapSaver : IBitmapSaver
    {
        public void SaveBitmapTo(
            Bitmap bitmap,
            string directory, string file, ImageFormat imageFormat,
            bool openAfterSave = false)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var fileAbsoluteName = Path.Combine(directory, file);

            bitmap.Save(fileAbsoluteName, imageFormat);

            if (openAfterSave)
                OpenImage(fileAbsoluteName);
        }

        private static void OpenImage(string fileAbsoluteName)
        {
            var info = new ProcessStartInfo(fileAbsoluteName);
            using (var process = new Process())
            {
                process.StartInfo = info;
                process.Start();
            }
        }
    }
}