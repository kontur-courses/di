using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class TagCloudVisualizer : IVisualizer
    {
        private readonly ILayouter layouter;
        private readonly IParser textParser;
        private readonly ITagPainter painter;

        public TagCloudVisualizer(IParser textParser, ILayouter layouter, ITagPainter painter)
        {
            this.textParser = textParser;
            this.layouter = layouter;
            this.painter = painter;
        }

        public Bitmap VisualizeTextFromFile(string fileName, ImageSettings imageSettings)
        {
            var text = TextRetriever.RetrieveTextFromFile(fileName);
            var wordTokens = textParser.ParseToTokens(text);
            var bmp = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
            var graphics = Graphics.FromImage(bmp);
            var tags = new List<Tag>();

            var cloudScale = 1f;
            foreach (var word in wordTokens)
            {
                var tag = CreateTag(graphics, word, imageSettings);
                var currentCloudScale = CalculateCloudScale(tag, imageSettings);

                if (currentCloudScale < cloudScale)
                    cloudScale = currentCloudScale;

                tags.Add(tag);
            }


            foreach (var tag in tags)
                DrawTag(graphics, tag, cloudScale, imageSettings);

            return bmp;
        }

        private float CalculateCloudScale(Tag tag, ImageSettings imageSettings)
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

        private Tag CreateTag(Graphics graphics, WordToken wordToken, ImageSettings imageSettings)
        {
            var fontSize = CalculateFontSize(wordToken, imageSettings);
            var wordFont = new Font(imageSettings.Font.FontFamily, fontSize, imageSettings.Font.Style);
            var wordRectangle = layouter.PutNextRectangle(CalculateWordSize(wordToken, wordFont, graphics));

            wordRectangle.Location = new Point(
                wordRectangle.X + imageSettings.CloudCenter.X, 
                wordRectangle.Y + imageSettings.CloudCenter.Y
                );

            var color = painter.GetTagColor();

            return new Tag(wordToken, wordRectangle, fontSize, color);
        }


        private void DrawTag(Graphics graphics, Tag tag, float cloudScale, ImageSettings imageSettings)
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

        private float CalculateFontSize(WordToken word, ImageSettings imageSettings)
        {
            return imageSettings.Font.Size + word.TextCount * 3;
        }
    }
}