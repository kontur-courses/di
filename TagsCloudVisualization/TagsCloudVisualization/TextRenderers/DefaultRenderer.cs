using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
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

        public Size GetRectangleSize(ImageSettings imageSettings, KeyValuePair<string, int> wordInfo)
        {
            using (var font = GetFont(imageSettings.Font, imageSettings.MinimalTextSize * wordInfo.Value))
                return TextRenderer.MeasureText(wordInfo.Key, font);
        }

        private Font GetFont(string font, float size) => new Font(font, size);
        

        public void PrintWords(int width, int height, Dictionary<string, Rectangle> info, ImageSettings imageSettings)
        {
            var colorSelector = new Random();
            using (var image = new Bitmap(width, height))
            using (var drawPlace = Graphics.FromImage(image))
            {
                foreach (var wordInfo in info)
                {
                    using (var font = GetFont(imageSettings.Font, wordInfo.Value.Width / wordInfo.Key.Length))
                    {
                        var color = imageSettings.Colors[colorSelector.Next(imageSettings.Colors.Count)];
                        drawPlace.DrawString(wordInfo.Key, font, new SolidBrush(color), wordInfo.Value, stringFormat);
                    }
                }

                image.Save(imageSettings.ImageName + imageSettings.ImageExtention);
            }
        }
    }
}
