using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer;

namespace TagsCloudBuilder
{
    public static class Drawer
    {
        public static void DrawAndSaveWords(Size canvasSize, List<ContainerInfo> containers, string name,
            ImageFormat imageFormat, bool debug = false, string path = "")
        {
            var stringFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            using (var bitmap = new Bitmap(canvasSize.Height, canvasSize.Width))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(Brushes.White, 0, 0, canvasSize.Height, canvasSize.Width);
                    foreach (var container in containers)
                    {
                        if (debug)
                            graphics.DrawRectangle(Pens.Black, container.Rectangle);
                        graphics.DrawString(
                            container.Text,
                            container.TextFont,
                            new SolidBrush(container.TextColor),
                            container.Rectangle,
                            stringFormat);
                    }

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
