using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Actions
{
    public class SaveImageAction : ISaveImageAction
    {
        public string CommandName => "- SaveImage";

        public void Perform(string path, Bitmap image)
        {
            image.Save(path, ImageFormat.Png);
        }
    }
}