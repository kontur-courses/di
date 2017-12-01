using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class CloudTagDrawer
    {
        private readonly IFileAnalyzer fileAnalyzer;
        private readonly ITagMaker tagMaker;
        private readonly IReader reader;
        private readonly int height;
        private readonly int width;

        public CloudTagDrawer(
            IFileAnalyzer fileAnalyzer,
            ITagMaker tagMaker,
            IReader reader,
            int height,
            int width
        )
        {
            this.fileAnalyzer = fileAnalyzer;
            this.tagMaker = tagMaker;
            this.reader = reader;
            this.height = height;
            this.width = width;
        }

        public void DrawTagsToFile(string filename)
        {
            var frequencyDict = fileAnalyzer.GetWordsFrequensy(reader.ReadWords());
            var tagRectangles = tagMaker.MakeTagRectangles(frequencyDict);
            
            var bitmap = DrawTagsOnBitmap(tagRectangles);
            bitmap.Save(filename);
        }

        public void DrawTagsToForm()
        {
            var frequencyDict = fileAnalyzer.GetWordsFrequensy(reader.ReadWords());
            var tagRectangles = tagMaker.MakeTagRectangles(frequencyDict);
            
            var bitmap = DrawTagsOnBitmap(tagRectangles);
            bitmap.ToForm();
        }


        private Bitmap DrawTagsOnBitmap(Dictionary<Rectangle, (string, Font)>tagRectangles)
        {
            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                var selPen = new Pen(Color.Blue);
                var selBrush = new SolidBrush(Color.Black);
                foreach (var tag in tagRectangles)
                {
                    g.DrawString(tag.Value.Item1, tag.Value.Item2, selBrush, tag.Key.X, tag.Key.Y);
                }
            }
            return bitmap;
        }
    }
    internal static class BitmapExtensions
    {
        public static void ToForm(this Bitmap bitmap)
        {
            Form aForm = new Form();
            aForm.Width = bitmap.Width;
            aForm.Height = bitmap.Height;
            aForm.BackgroundImage = bitmap;
            aForm.ShowDialog();
        } 
    }
}