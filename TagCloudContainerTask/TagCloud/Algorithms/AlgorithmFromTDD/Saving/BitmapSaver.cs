using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagCloudTask.Saving
{
    public class BitmapSaver : IBitmapSaver
    {
        private const string BitmapsDirectory = "layouts";
        private const string FileExt = ".png";
        private const string FileNamePrefix = "TagCloud_";
        private static readonly ImageFormat ImgFormat = ImageFormat.Png;

        private static readonly string ProjectDirectory
            = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        public string Save(Bitmap bitmap, bool openAfterSave)
        {
            var savePath = Path.Combine(ProjectDirectory, BitmapsDirectory);
            var fullFileName = GetFileName();
            var absoluteFileName = Path.Combine(savePath, fullFileName);

            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            bitmap.Save(absoluteFileName, ImgFormat);

            if (openAfterSave)
                OpenImage(absoluteFileName);

            return absoluteFileName;
        }

        private static string GetFileName()
        {
            var currentTime = DateTime.Now.ToString("hh-mm-ss-fff");
            return string.Join("", FileNamePrefix, currentTime, FileExt);
        }

        private static void OpenImage(string absoluteFileName)
        {
            var info = new ProcessStartInfo(absoluteFileName);
            using (var process = new Process())
            {
                process.StartInfo = info;
                process.Start();
            }
        }
    }
}