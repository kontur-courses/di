using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TagsCloudVisualization
{
    public static class Drawer
    {
        public static void DrawAndSaveRectangles(Size canvasSize, List<Rectangle> rectangles, string name, string path = "")
        {
            using (var bitmap = new Bitmap(canvasSize.Height, canvasSize.Width))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(Brushes.White, 0, 0, 1000, 1000);
                    foreach (var rect in rectangles)
                        graphics.DrawRectangle(Pens.Black, rect);

                    try
                    {
                        bitmap.Save($"{path}{name}", ImageFormat.Png);
                    }
                    catch (ExternalException e)
                    {
                        Console.Error.WriteLine("Something gone wrong. Check the path correctness.", e);
                    }
                }
            }
        }
    }
}
