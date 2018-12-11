using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace СircularCloudTesting
{
    public static class ExtensionMethods
    {
        public static byte[] GetColorValues(this BitmapData bmpData)
        {
            var size = Math.Abs(bmpData.Stride) * bmpData.Height;
            var ptr = bmpData.Scan0;
            var colorValues = new byte[size];

            Marshal.Copy(ptr, colorValues, 0, size);
            return colorValues;
        }
        public static T WithBitmapData<T>(this Bitmap bmp, Func<BitmapData, T> bitmapDataAction)
        {
            var rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bitmapData = bmp.LockBits(rectangle, ImageLockMode.ReadWrite, bmp.PixelFormat);
            try
            {
                return bitmapDataAction(bitmapData);
            }
            finally
            {
                bmp.UnlockBits(bitmapData);
            }
        }
    }
}