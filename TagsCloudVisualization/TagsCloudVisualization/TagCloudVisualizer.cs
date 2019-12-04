using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class TagCloudVisualizer : IVisualizer
    {
        private readonly ILayouter layouter;
        private readonly ImageSettings imageSettings;
        private readonly IParser textParser;

        public TagCloudVisualizer(IParser textParser, ILayouter layouter, ImageSettings imageSettings)
        {
            this.textParser = textParser;
            this.layouter = layouter;
            this.imageSettings = imageSettings;
        }

        public Bitmap VisualizeTextFromFile(string fileName)
        {
            var text = TextRetriever.RetrieveTextFromFile(fileName);
            var wordTokens = textParser.ParseToTokens(text);
            var bmp = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
            var graphics = Graphics.FromImage(bmp);
            var tags = new List<Tag>();

            var cloudScale = 1f;
            foreach (var word in wordTokens)
            {
                var tag = CreateTag(graphics, word);
                var currentCloudScale = CalculateCloudScale(tag);

                if (currentCloudScale < cloudScale)
                    cloudScale = currentCloudScale;

                tags.Add(tag);
            }

            imageSettings.Painter.SetColorsForTagCollection(tags);

            foreach (var tag in tags)
                DrawTag(graphics, tag, cloudScale);

            return bmp;
        }

        private float CalculateCloudScale(Tag tag)
        {
            var realFigureCenter =
                new Point(
                    tag.TagBox.X + tag.TagBox.Width / 2,
                    tag.TagBox.Y + tag.TagBox.Height / 2
                );

            var posMinusCenter = new Point(
                realFigureCenter.X - imageSettings.CloudCenter.X,
                realFigureCenter.Y - imageSettings.CloudCenter.Y
            );

            var cloudBorderSize = new Size(
                Math.Min(
                    imageSettings.CloudCenter.X,
                    imageSettings.ImageSize.Width - imageSettings.CloudCenter.X
                ) * 2,
                Math.Min(
                    imageSettings.CloudCenter.Y,
                    imageSettings.ImageSize.Height - imageSettings.CloudCenter.Y
                ) * 2
            );

            var distanceToImageBorder = Geometry
                .GetLengthFromRectangleCenterToBorderOnVector(
                    new Rectangle(Point.Empty, cloudBorderSize),
                    posMinusCenter
                );

            return distanceToImageBorder == 0 ? 1 : (float) (distanceToImageBorder / posMinusCenter.GetLength());
        }

        private static Size CalculateWordSize(WordToken wordToken, Font font, Graphics graphics)
        {
            var size = graphics.MeasureString(wordToken.Word, font);
            return size.ToSize();
        }

        private Tag CreateTag(Graphics graphics, WordToken wordToken)
        {
            var fontSize = CalculateFontSize(wordToken);
            var wordFont = new Font(imageSettings.Font.FontFamily, fontSize, imageSettings.Font.Style);
            var wordRectangle = layouter.PutNextRectangle(CalculateWordSize(wordToken, wordFont, graphics));

            return new Tag(wordToken, wordRectangle, fontSize);
        }

        private void DrawTag(Graphics graphics, Tag tag, float cloudScale)
        {
            var scaledFontSize = tag.FontSize * cloudScale;

            var scaledFont = new Font(
                imageSettings.Font.FontFamily,
                scaledFontSize,
                imageSettings.Font.Style
            );

            var positionMinusCenter = new Point(
                tag.TagBox.Location.X - imageSettings.CloudCenter.X,
                tag.TagBox.Location.Y - imageSettings.CloudCenter.Y
            );

            var newPointLocation = new Point(
                imageSettings.CloudCenter.X + (int) (positionMinusCenter.X * cloudScale),
                imageSettings.CloudCenter.Y + (int) (positionMinusCenter.Y * cloudScale)
            );

            graphics.DrawString(tag.WordToken.Word, scaledFont, new SolidBrush(tag.Color), newPointLocation);
        }

        private float CalculateFontSize(WordToken word)
        {
            return imageSettings.Font.Size + word.TextCount * 3;
        }
    }
}