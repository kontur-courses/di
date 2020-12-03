using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    class TagsCloudDrawer : ITagsCloudDrawer
    {
        private IRectanglesLayouter layouter;
        private readonly TagsCloudSettings tagsCloudSettings;

        public TagsCloudDrawer(IRectanglesLayouter layouter, TagsCloudSettings tagsCloudSettings)
        {
            this.layouter = layouter;
            this.tagsCloudSettings = tagsCloudSettings;
        }

        public Image GetTagsCloud(IEnumerable<Tag> tags)
        {
            if (tagsCloudSettings.CloudToImageScaleRatio <= 0 || tagsCloudSettings.CloudToImageScaleRatio > 1)
                throw new ArgumentException("ratio should be positive and be less 1");
            var tagsCloudWords = new List<TagscloudWord>();
            var tagsCloudsBounds = Rectangle.Empty;
            foreach (var tag in tags)
            {
                var wordFont = new Font(tagsCloudSettings.CurrentFontFamily, tag.Weight, tagsCloudSettings.CurrentFontStyle);
                var newRectangle = layouter.PutNextRectangle(ConvertWordToSize(tag.Word, wordFont));
                tagsCloudWords.Add(new TagscloudWord(tag.Word, wordFont, newRectangle.Location));
                TryExpandTagsCloud(ref tagsCloudsBounds, newRectangle);
            }
            var newSize = new ImageSize((int)(tagsCloudSettings.ImageSize.Width * tagsCloudSettings.CloudToImageScaleRatio), 
                (int)(tagsCloudSettings.ImageSize.Height * tagsCloudSettings.CloudToImageScaleRatio));
            var newRatio = CalculateRatio(tagsCloudsBounds.Size, newSize);
            for (var i = 0; i < tagsCloudWords.Count; i++)
            {
                tagsCloudWords[i] = new TagscloudWord(tagsCloudWords[i].Value, 
                    new Font(tagsCloudWords[i].Font.FontFamily, 
                        (int)(tagsCloudWords[i].Font.Size * newRatio), tagsCloudWords[i].Font.Style),
                    new Point((int)(tagsCloudWords[i].Position.X * newRatio), (int)(tagsCloudWords[i].Position.Y * newRatio)));
            }
            layouter.Clear();
            var layouterCenterDelta = new Size(tagsCloudsBounds.X + tagsCloudsBounds.Width / 2, 
                tagsCloudsBounds.Y + tagsCloudsBounds.Height / 2) * newRatio;
            return DrawTagscloud(tagsCloudWords, tagsCloudSettings, 
                new PointF((float)tagsCloudSettings.ImageSize.Width / 2, 
                    (float)tagsCloudSettings.ImageSize.Height / 2) - layouterCenterDelta);
        }

        public void SetNewLayouter(IRectanglesLayouter newLayouter)
        {
            layouter = newLayouter;
        }

        private static float CalculateRatio(Size tagsCloudSize, ImageSize imageSize)
        {
            if ((double)tagsCloudSize.Width / imageSize.Width > (float)tagsCloudSize.Height / imageSize.Height)
                return (float) imageSize.Width / tagsCloudSize.Width;
            return (float) imageSize.Height / tagsCloudSize.Height;
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

        private void TryExpandTagsCloud(ref Rectangle bounds, Rectangle newRectangle)
        {
            var maxX = Math.Max(bounds.Location.X + bounds.Width, newRectangle.Location.X + newRectangle.Width);
            var maxY = Math.Max(bounds.Location.Y + bounds.Height, newRectangle.Location.Y + newRectangle.Height);
            var minX = Math.Min(bounds.Location.X, newRectangle.Location.X);
            var minY = Math.Min(bounds.Location.Y, newRectangle.Location.Y);
            bounds = new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }

        private Size ConvertWordToSize(string word, Font font)
        {
            var floatSize = Graphics.FromImage(new Bitmap(10, 10)).MeasureString(word, font);
            return new Size((int)Math.Ceiling((decimal)floatSize.Width), (int)Math.Ceiling((decimal)floatSize.Height));
        }
    }
}
