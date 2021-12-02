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
            Bitmap = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
            Graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
        }

        public void Dispose()
        {
            Bitmap?.Dispose();
            Graphics?.Dispose();
        }
    }
}