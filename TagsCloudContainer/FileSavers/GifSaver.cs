using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.FileSavers
{
    public class GifSaver : IFileSaver
    {
        public void SaveCanvas(string pathToSave, Bitmap canvas)
        {
            canvas.Save(pathToSave + ".gif", ImageFormat.Gif);
            Console.WriteLine($"File was saved to {pathToSave}.gif", pathToSave);
        }
    }
}