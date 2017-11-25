using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public static class CloudTagDrawer
    {
        public static void DrawTagsToFile(Point cloudCenter, Dictionary<Rectangle, (string, Font)> tagsDict, string name, int width, int height)
        {
            using (var bitmap = new Bitmap(width, height))
            {
                DrawTagsOnBitmap(cloudCenter, tagsDict, bitmap);
                bitmap.Save(name);
            }
        }

        public static void DrawTagsToForm(Point cloudCenter, Dictionary<Rectangle, (string, Font)> tagsDict, int width, int height)
        {
            using (var bitmap = new Bitmap(width, height))
            {
                DrawTagsOnBitmap(cloudCenter, tagsDict, bitmap);
                Form aForm = new Form();
                aForm.Width = 800;
                aForm.Height = 800;
                aForm.BackgroundImage = bitmap;
                aForm.ShowDialog();
            }
        }

        private static void DrawTagsOnBitmap(Point cloudCenter, Dictionary<Rectangle, (string, Font)> tagsDict, Bitmap bitmap)
        {
            using (var g = Graphics.FromImage(bitmap))
            {
                var selPen = new Pen(Color.Blue);
                var selBrush = new SolidBrush(Color.Black);
                g.DrawRectangle(new Pen(Color.Red), (int) cloudCenter.X, (int) cloudCenter.Y, 1, 1);
                foreach (var tag in tagsDict)
                {
                    g.DrawRectangle(selPen, tag.Key);
                    g.DrawString(tag.Value.Item1, tag.Value.Item2, selBrush, tag.Key.X, tag.Key.Y);
                }
            }
        }
        private static void DrawRectanglesOnBitmap(Point cloudCenter, List<Rectangle> rectangles, Bitmap bitmap)
        {
            using (var g = Graphics.FromImage(bitmap))
            {
                var selPen = new Pen(Color.Blue);
                var selBrush = new SolidBrush(Color.Black);
                g.DrawRectangle(new Pen(Color.Red), (int)cloudCenter.X, (int)cloudCenter.Y, 1, 1);
                foreach (var rectangle in rectangles)
                    g.DrawRectangle(selPen, rectangle);

            }
        }

        public static void DrawRectanglesToFile(Point cloudCenter,List<Rectangle> rectangles, string name, int width, int height)
        {
            using (var bitmap = new Bitmap(width, height))
            {
                DrawRectanglesOnBitmap(cloudCenter, rectangles, bitmap);
                bitmap.Save(name);
            }
        }

    }
}