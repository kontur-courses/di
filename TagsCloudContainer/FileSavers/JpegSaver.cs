using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.FileSavers
{
    public class JpegSaver:IFileSaver
    
    {
        public void SaveCanvas(string pathToSave, Bitmap canvas)
        {
            canvas.Save(pathToSave + ".jpeg", ImageFormat.Jpeg);
            Console.WriteLine($"File was saved to {pathToSave}.jpeg", pathToSave);
        }
    }
}