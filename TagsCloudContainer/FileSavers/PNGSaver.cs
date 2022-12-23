using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer.FileSavers
{
    public class PngSaver : IFileSaver
    {
        public void SaveCanvas(string pathToSave, Bitmap canvas)
        {
            canvas.Save(pathToSave + ".png", ImageFormat.Png);
            Console.WriteLine($"File was saved to {pathToSave}.png", pathToSave);
        }
    }
}