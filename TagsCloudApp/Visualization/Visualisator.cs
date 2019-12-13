using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp.Visualization
{
    public class Visualisator: IVisualisator
    {
        public Bitmap Visualize(IEnumerable<Tuple<string, Rectangle>> wordRectanglePairs, Size bitmpapSize
                                , Color currentColor, Font currentFont)
        {            
            Bitmap bitmap = new Bitmap(bitmpapSize.Height, bitmpapSize.Width);
            Graphics g = Graphics.FromImage(bitmap);
            var pen = new Pen(currentColor, 2);
            foreach (var wordRectangle in wordRectanglePairs)
            {                
                Font font = new Font(currentFont.SystemFontName, wordRectangle.Item2.Height / 2);
                var brush = new SolidBrush(pen.Color);                
                g.DrawString(wordRectangle.Item1, font, brush, wordRectangle.Item2);

            }
            return bitmap;
        }
    }
}
