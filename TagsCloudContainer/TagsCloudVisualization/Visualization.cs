using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.WordTagsCloud;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class Visualization : IVisualization
    {
        private readonly ICloudSettings _cloudSettings;

        public Visualization(ICloudSettings cloudSettings)
        {
            _cloudSettings = cloudSettings;
        }

        public Bitmap GetBitmapImageCloud(int cloudRadius, List<WordTag> tags)
        {
            if (tags.Count == 0)
                throw new Exception("Doesn't contain tags for draw");
            var realImageSize = new Size(cloudRadius * 2, cloudRadius * 2);
            var customImageSize = _cloudSettings.GetParameterValue<Size>(ParameterType.ImageSize);
            if (realImageSize.Height > customImageSize.Height || realImageSize.Width > customImageSize.Width)
            {
                if (!customImageSize.IsEmpty)
                    Console.WriteLine("Real cloud sizes are used, but custom ones don't fit");
                customImageSize = realImageSize;
            }

            var bitmap =
                new Bitmap(customImageSize.Width, customImageSize.Height);
            DrawCloud(bitmap, tags, customImageSize);
            return bitmap;
        }

        private void DrawCloud(Bitmap bitmap, List<WordTag> tags, Size imageSize)
        {
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(_cloudSettings.GetParameterValue<Color>(ParameterType.BackgroundColor));
            foreach (var tag in tags)
            {
                var rectangle = tag.Rectangle;
                rectangle.Offset(imageSize.Width / 2, imageSize.Height / 2);
                graphics.DrawString(tag.Text, tag.WordFont,
                    new SolidBrush(_cloudSettings.GetParameterValue<Color>(ParameterType.TextColor)), rectangle);
            }

            graphics.Flush();
        }
    }
}