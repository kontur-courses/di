using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Visualizers
{
    public class DrawingSettings
    {
        public Brush PenColor { get; }
        public Brush BackgroundColor { get; }
        //public int Width { get; }
        //public int Height { get; }
        public Bitmap Bitmap { get; }
        public Graphics Graphics { get; }

        public DrawingSettings(Color penColor, Color backgroundColor, int width, int height)
        {
            PenColor = new SolidBrush(penColor);
            BackgroundColor = new SolidBrush(backgroundColor);
            //Width = width;
            //Height = height;
            if (width < 0 || height < 0)
                throw new ArgumentException("Width and height should be positive but was: " +
                                            $"Width = {width} " +
                                            $"Height = {height}");
            Bitmap = new Bitmap(width, height);
            Graphics = Graphics.FromImage(Bitmap);
        }
    }
}
