using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TagsCloudContainer.BitmapSaver
{
    public class BitmapSaver : IBitmapSaver
    {
        private readonly HashSet<string> allowedExt = new()
        {
            ".png",
            ".jpeg",
            ".jpg",
            ".gif",
            ".bmp",
            ".icon"
        };

        public void Save(Bitmap bmp, string fullPathWithExt)
        {
            var ext = Path.GetExtension(fullPathWithExt);
            var directory = Path.GetDirectoryName(fullPathWithExt);
            if (!allowedExt.Contains(ext))
                throw new ArgumentException($"file {fullPathWithExt} has wrong image extension {ext}");
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            try
            {
                bmp.Save(fullPathWithExt);
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось сохранить файл", e);
            }
        }
    }
}