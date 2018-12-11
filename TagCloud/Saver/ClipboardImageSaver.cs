using System.Drawing;
using System.Windows.Forms;

namespace TagCloud.Saver
{
    public class ClipboardImageSaver : IImageSaver
    {
        public void Save(Image image, string fileName)
        {
            Clipboard.SetImage(image);
        }
    }
}