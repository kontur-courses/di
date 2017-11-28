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
        private readonly ITagHandler tagHandler;
        private readonly IReader reader;
        private readonly int height;
        private readonly int width;

        public CloudTagDrawer(
            IFileAnalyzer fileAnalyzer,
            ITagHandler tagHandler,
            IReader reader,
            int height,
            int width
        )
        {
            this.fileAnalyzer = fileAnalyzer;
            this.tagHandler = tagHandler;
            this.reader = reader;
            this.height = height;
            this.width = width;
        }
        
        public Bitmap DrawTags()
        {
            var frequencyDict = fileAnalyzer.GetWordsFrequensy(reader.ReadWords());
            var tagRectangles = tagHandler.MakeTagRectangles(frequencyDict);
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
}