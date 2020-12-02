using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    class TagsCloudDrawer : ITagsCloudDrawer
    {
        private IRectanglesLayouter constellator;

        public TagsCloudDrawer(IRectanglesLayouter constellator)
        {
            this.constellator = constellator;
        }

        public Image GetTagsCloud(Dictionary<string, int> words, TagsCloudSettings settings, double cloudToImageScaleRatio)
        {
            if (cloudToImageScaleRatio <= 0 || cloudToImageScaleRatio > 1)
                throw new ArgumentException("ratio should be positive and be less 1");
            var tagscloudWords = new List<TagscloudWord>();
            foreach (var word in words)
            {
                var wordFont = new Font(settings.CurrentFontFamily, word.Value * 10, settings.CurrentFontStyle);
                tagscloudWords.Add(new TagscloudWord(
                    word.Key, wordFont, 
                    constellator.PutNextRectangle(
                        ConvertWordToSize(word.Key, wordFont)).Location));
            }
            var newSize = new ImageSize((int)(settings.ImageSize.Height * cloudToImageScaleRatio), 
                (int)(settings.ImageSize.Width * cloudToImageScaleRatio));
            var newRatio = CalculateRatio(constellator, newSize);
            var constellatorCenterDelta = new SizeF((float)(constellator.MaxX + constellator.MinX) / 2, 
                (float)(constellator.MaxY + constellator.MinY) / 2) * newRatio;
            for (var i = 0; i < tagscloudWords.Count; i++)
            {
                tagscloudWords[i] = new TagscloudWord(tagscloudWords[i].Value, 
                    new Font(tagscloudWords[i].Font.FontFamily, 
                        (int)(tagscloudWords[i].Font.Size * newRatio), tagscloudWords[i].Font.Style),
                    new Point((int)(tagscloudWords[i].Position.X * newRatio), (int)(tagscloudWords[i].Position.Y * newRatio)));
            }
            constellator.Clear();
            return DrawTagscloud(tagscloudWords, settings, 
                new PointF((float)settings.ImageSize.Width / 2, (float)settings.ImageSize.Height / 2) - constellatorCenterDelta);
        }

        public void SetNewLayouter(IRectanglesLayouter newConstellator)
        {
            constellator = newConstellator;
        }

        private static float CalculateRatio(IRectanglesLayouter constellator, ImageSize newSize)
        {
            if ((double) constellator.Width / newSize.Width > (float) constellator.Height / newSize.Height)
                return (float) newSize.Width / constellator.Width;
            return (float) newSize.Height / constellator.Height;
        }

        private Image DrawTagscloud(IEnumerable<TagscloudWord> words, TagsCloudSettings settings, PointF center)
        {
            var image = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            var graphics = Graphics.FromImage(image);
            graphics.TranslateTransform(center.X, center.Y);
            graphics.Clear(settings.Palette.BackgroundColor);
            foreach (var word in words)
            {
                graphics.DrawString(word.Value, word.Font, 
                    new SolidBrush(settings.Palette.PrimaryColor), word.Position);
            }
            return image;
        }

        private Size ConvertWordToSize(string word, Font font)
        {
            var floatSize = Graphics.FromImage(new Bitmap(10, 10)).MeasureString(word, font);
            return new Size((int)Math.Ceiling((decimal)floatSize.Width), (int)Math.Ceiling((decimal)floatSize.Height));
        }
    }
}
