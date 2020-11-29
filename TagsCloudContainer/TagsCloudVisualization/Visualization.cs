using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.WordTagsCloud;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class Visualization
    {
        private readonly ICloudSettings _cloudSettings;

        public Visualization(ICloudSettings cloudSettings)
        {
            _cloudSettings = cloudSettings;
        }

        public void GetBitmapImageCloud(int cloudRadius, List<WordTag> tags)
        {
            if (tags.Count == 0)
                throw new Exception("Doesn't contain tags for draw");
            var realImageSize = new Size(cloudRadius * 2, cloudRadius * 2);
            var customImageSize = _cloudSettings.GetParameterValue<Size>(ParameterType.ImageSize);
            if (realImageSize.Height > customImageSize.Height || realImageSize.Width > customImageSize.Width)
                throw new Exception("Can't draw a cloud of the custom size");
            using var bitmap =
                new Bitmap(customImageSize.Width, customImageSize.Height);
            DrawCloud(bitmap, tags, customImageSize);
            bitmap.Save(_cloudSettings.GetParameterValue<string>(ParameterType.PathToSave), ImageFormat.Png);
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