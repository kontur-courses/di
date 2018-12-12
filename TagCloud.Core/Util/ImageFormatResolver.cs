using System;
using System.Drawing.Imaging;
using System.Linq;

namespace TagCloud.Core.Util
{
    public class ImageFormatResolver
    {
        public static bool TryResolveFromFileName(string fileName, out ImageFormat imageFormat)
        {
            imageFormat = null;
            var prop = typeof(ImageFormat).GetProperties().FirstOrDefault(
                p => fileName.EndsWith(p.Name, StringComparison.InvariantCultureIgnoreCase));
            if (prop == null)
                return false;
            imageFormat = prop.GetValue(prop) as ImageFormat;
            return true;
        }
    }
}