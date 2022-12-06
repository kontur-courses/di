using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagCloudHelper
    {
        public static Bitmap DrawTagCloud(Dictionary<Tag, Rectangle> tags, CloudConfiguration cloudConfiguration)
        {
            var imageWidth = cloudConfiguration.ImageSize.Width;
            var imageHeight = cloudConfiguration.ImageSize.Height;

            var bitmap = new Bitmap(imageWidth, imageHeight);
            var gp = Graphics.FromImage(bitmap);
            
            gp.FillRectangle(new SolidBrush(cloudConfiguration.BackgroundColor), new Rectangle(0,0, imageWidth, imageHeight));

            foreach (var tag in tags)
                DrawStringInside(gp, tag.Value, new Font(cloudConfiguration.FontFamily, 1), new SolidBrush(cloudConfiguration.PrimaryColor), tag.Key.Text);

            return bitmap;
        }
        
        public static Bitmap DrawOnlyRectangles(List<Rectangle> rects, CloudConfiguration cloudConfiguration)
        {
            var imageWidth = cloudConfiguration.ImageSize.Width;
            var imageHeight = cloudConfiguration.ImageSize.Height;
            
            var bitmap = new Bitmap(imageWidth, imageHeight);
            var gp = Graphics.FromImage(bitmap);
            
            gp.FillRectangle(new SolidBrush(cloudConfiguration.BackgroundColor), new Rectangle(0,0, imageWidth, imageHeight));
            gp.DrawRectangles(new Pen(cloudConfiguration.PrimaryColor), rects.ToArray());
            
            return bitmap;
        }

        public static List<Tag> CreateTagsFromWords(List<string> words, int minFontSize = 10, int maxFontSize = 50)
        {
            var tags = new List<Tag>();
            var dict = new Dictionary<string, int>();
            
            foreach (var word in words)
            {
                if (!dict.ContainsKey(word))
                    dict[word] = 0;
                dict[word]++;
            }

            var minFrequency = dict.Min(x => x.Value);
            var maxFrequency = dict.Max(x => x.Value);

            foreach (var (text, frequency) in dict)
            {
                var fontSize = maxFontSize * (frequency - minFrequency) / (maxFrequency - minFrequency);

                if (fontSize < minFontSize)
                    fontSize = minFontSize;
                
                tags.Add(new Tag(text, fontSize, frequency));
            }

            return tags;
        }

        public static List<Size> GenerateRectangleSizes(List<Tag> tags)
        {
            var gp = Graphics.FromImage(new Bitmap(1, 1));

            return tags.Select(tag => gp.MeasureString(tag.Text, new Font("Arial", tag.FontSize)).ToSize()).ToList();
        }

        public static List<Size> GenerateRectangleSizesRandom(
            int amount, int minWidth = 20, int maxWidth = 150, int minHeight = 20, int maxHeight = 100)
        {
            var rnd = new Random();
            var listSize = new List<Size>();
            
            for (var i = 0; i < amount; i++)
                listSize.Add(new Size(rnd.Next(minWidth, maxWidth), rnd.Next(minHeight, maxHeight)));

            return listSize;
        }
        
        private static void DrawStringInside(Graphics graphics, Rectangle rect, Font font, Brush brush, string text)
        {
            var textSize = graphics.MeasureString(text, font);
            var state = graphics.Save();
            graphics.TranslateTransform(rect.Left, rect.Top);
            graphics.ScaleTransform(rect.Width / textSize.Width, rect.Height / textSize.Height);
            graphics.DrawString(text, font, brush, PointF.Empty);
            graphics.Restore(state);
        }
    }
}