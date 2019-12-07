using System.Drawing.Imaging;

namespace TagsCloudContainer.CloudVisualizers.ImageSaving
{
    public static class SupportedImageFormats
    {
        public static ImageFormat TryGetSupportedImageFormats(string name)
        {
            var lower = name.ToLower();
            switch (lower)
            {
                default:
                    return null;
                case "png":
                    return ImageFormat.Png;
                case "jpeg":
                case "jpg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
            }
        }
        
    }
}