using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer
{
    public class TagsLayouter
    {
        private CircularCloudLayouter cloudLayouter;
        private readonly string[] tags;
        private readonly Font font;
        public Dictionary<string, Rectangle> Tags { get; set; }

        public TagsLayouter(CircularCloudLayouter cloudLayouter, string[] tags, Font font)
        {
            this.cloudLayouter = cloudLayouter;
            this.tags = tags;
            this.font = font;
            Tags = new Dictionary<string, Rectangle>();
        }

        public void PutAllTags()
        {
            foreach (var tag in tags)
                PutNextTags(tag);
        }

        private void PutNextTags(string tag)
        {
            var sizeF = CalculateSizeWord(tag);
            var size = new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
            var rect = cloudLayouter.PutNextRectangle(size);
            Tags[tag] = rect;
        }

        private SizeF CalculateSizeWord(string word)
        {
            var bmp = new Bitmap(100, 100);
            using Graphics gph = Graphics.FromImage(bmp);
            return gph.MeasureString(word, font);
        }

        public void SaveBitmapWithText(string btmName)
        {
            var bmp = new Bitmap(800, 500);
            using Graphics gph = Graphics.FromImage(bmp);
            var brush = new SolidBrush(Color.Black);
            foreach (var tag in Tags)
            {
                gph.DrawString(tag.Key, font, brush, tag.Value);
            }
            bmp.Save(btmName + ".bmp");
        }
    }
}
