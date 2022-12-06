using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TagsCloudContainer
{
    public class TagsLayouter
    {
        private CircularCloudLayouter cloudLayouter;
        private readonly Dictionary<string, int> tags;
        private readonly FontFamily fontFamily;
        private readonly SolidBrush brush;
        private readonly Bitmap bitmap;
        private readonly float maxFontSize;
        public List<TagInfo> Tags;


        public TagsLayouter(CircularCloudLayouter cloudLayouter, Dictionary<string, int> tags, FontFamily fontFamily
            , float maxFontSize, SolidBrush brush, Bitmap bitmap)
        {
            this.cloudLayouter = cloudLayouter;
            this.tags = tags;
            this.fontFamily = fontFamily;
            this.maxFontSize = maxFontSize;
            this.brush = brush;
            this.bitmap = bitmap;
        }

        public void PutAllTags()
        {
            var minT = tags.Values.Min();
            var maxT = tags.Values.Max();
            Tags = tags
                .Select(t => new TagInfo(t.Key, new Font(fontFamily, CalculateSizeFont(t.Value, minT, maxT, maxFontSize))))
                .ToList();

            foreach (var tag in Tags)
            {
                var sizeF = CalculateSizeWord(tag.TagText,tag.TagFont);
                var size = new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
                var rect = cloudLayouter.PutNextRectangle(size);
                tag.TagRect = rect;
            }
        }

        private float CalculateSizeFont(int T,int minT, int maxT, float f)
        {
            return Math.Max(f * (T - minT) / (maxT - minT) , 1);
        }

        private SizeF CalculateSizeWord(string word, Font font)
        {
            using Graphics gph = Graphics.FromImage(bitmap);
            return gph.MeasureString(word, font);
        }

        public void SaveBitmapWithText(string btmName)
        {
            using Graphics gph = Graphics.FromImage(bitmap);
            foreach (var tag in Tags)
            {
                gph.DrawString(tag.TagText, tag.TagFont, brush, tag.TagRect);
            }
            bitmap.Save(btmName + ".png");
        }
    }
}
