using System;
using System.Drawing;

namespace TagsCloudContainer.Rendering
{
    public class Canvas : IDisposable
    {
        public Bitmap Bitmap { get; }
        public Graphics Graphics { get; }

        public Canvas(Bitmap bitmap, Graphics graphics)
        {
            Bitmap = bitmap;
            Graphics = graphics;
        }

        public void Dispose()
        {
            Bitmap.Dispose();
            Graphics.Dispose();
        }
    }
}