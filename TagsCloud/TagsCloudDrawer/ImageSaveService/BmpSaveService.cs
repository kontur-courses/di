using System.Drawing.Imaging;

namespace TagsCloudDrawer.ImageSaveService
{
    public class BmpSaveService : BaseFormatsImageSaveService
    {
        protected override string Extensions => "bmp";
        protected override ImageFormat Format => ImageFormat.Bmp;
    }
}