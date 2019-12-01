using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Palettes;
using TagsCloudContainer.CloudLayouters;

namespace TagsCloudContainer.Visualizers
{
    class SimpleVsualizer : IVisualizer
    {
        private IPalette palette;
        private ICloudLayouter cloudLayouter;

        public SimpleVsualizer(IPalette palette, ICloudLayouter cloudLayouter)
        {
            this.palette = palette;
            this.cloudLayouter = cloudLayouter;
        }

        public Bitmap VisualizeCloud(List<WordToken> wordTokens)
        {
            var tags = wordTokens
                .Select(wordToken => new TagToken(
                    wordToken,
                    cloudLayouter.PutNextRectangle(
                        new Size(
                            (int)Math.Round(palette.Font.Size * wordToken.Count * wordToken.Word.Length),
                            palette.Font.Height * wordToken.Count))))
                .ToList();

            var size = GetSizeBitmapFromTagTokens(tags);
            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var tag in tags)
            {
                graphics.DrawString(
                    tag.Word,
                    new Font(
                        palette.Font.Name,
                        palette.Font.Size * tag.Count),
                    palette.Brush,
                    tag.Rectangle.X - size.X,
                    tag.Rectangle.Y - size.Y);
            }

            return bitmap;
        }

        private static Rectangle GetSizeBitmapFromTagTokens(List<TagToken> tags)
        {
            var minTop = tags.Min(t => t.Rectangle.Top);
            var maxBottom = tags.Max(t => t.Rectangle.Bottom);
            var minLeft = tags.Min(t => t.Rectangle.Left);
            var maxRight = tags.Max(t => t.Rectangle.Right);
            return new Rectangle(minLeft, minTop, maxRight - minLeft, maxBottom - minTop);
        }
    }
}
