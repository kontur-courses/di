using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.TextRenderers
{
    public class DefaultRenderer: ITextRenderer
    {
        private readonly static StringFormat stringFormat = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        public void PrintWords(Bitmap image, Dictionary<string, (RectangleF rectangle, Font font)> info, ImageSettings imageSettings)
        {
            var colorSelector = new Random();
            using (var drawPlace = Graphics.FromImage(image))
            {
                foreach (var wordInfo in info)
                {
                    using (var font = wordInfo.Value.font)
                    {
                        var color = imageSettings.Colors[colorSelector.Next(imageSettings.Colors.Count)];
                        drawPlace.DrawString(wordInfo.Key, font, new SolidBrush(color), wordInfo.Value.rectangle, stringFormat);
                    }
                }

                image.Save(imageSettings.ImageName + imageSettings.ImageExtention);
            }
        }
    }
}
