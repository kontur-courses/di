using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization
{
    public class TagCloudRenderer
    {
        public void DrawCloud(RectangleF[] rectangles, VisualizingSettings settings)
        {
            ImageScaler imageScaler = new ImageScaler();
            var smallestSizeOfRectangles = imageScaler.GetMinPoints(rectangles);
            var unscaledImageSize = imageScaler.GetImageSizeWithRealSizeRectangles(rectangles, smallestSizeOfRectangles);    
          
            if (!imageScaler.NeedScale(settings, unscaledImageSize))
            {  
                using var unscaledBitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
                using var graphics = Graphics.FromImage(unscaledBitmap);
                using var pen = new Pen(settings.PenColor);
                graphics.Clear(settings.BackgroundColor);
                graphics.DrawRectangles(pen, rectangles);
                unscaledBitmap.Save(settings.ImageName + ".png", ImageFormat.Png);
                return;
            }
                     
            var bitmap = imageScaler.DrawScaleCloud(settings, rectangles, unscaledImageSize, smallestSizeOfRectangles);
            bitmap.Save(settings.ImageName + ".png", ImageFormat.Png);
        }    
    }
}