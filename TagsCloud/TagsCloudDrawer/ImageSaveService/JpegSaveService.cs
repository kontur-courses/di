using System.Drawing.Imaging;

namespace TagsCloudDrawer.ImageSaveService
{
    public class JpegSaveService : BaseFormatsImageSaveService
    {
        protected override string Extensions => "jpeg";
        protected override ImageFormat Format => ImageFormat.Jpeg;
    }
}