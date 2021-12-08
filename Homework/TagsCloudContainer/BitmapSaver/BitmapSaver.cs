using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer.BitmapSaver
{
    public class BitmapSaver : IBitmapSaver
    {
        public void Save(Bitmap bmp, DirectoryInfo directory, string fileName,ImageFormat format)
        {
            if(!directory.Exists)
                directory.Create();
            var fullPath = Path.Combine(directory.FullName, $"{fileName}.{format.ToString().ToLower()}");
            try
            {
                bmp.Save(fullPath, format);
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось сохранить файл", e);
            }
        }
    }
}