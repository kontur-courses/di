using System.Drawing.Imaging;

namespace TagsCloudDrawer.ImageSaveService
{
    public class PngSaveService : BaseFormatsImageSaveService
    {
        protected override string Extensions => "png";
        protected override ImageFormat Format => ImageFormat.Png;
    }
}