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
            graphics.FillRectangle(new SolidBrush(imageSettings.BackgroundColor), 0, 0, bmp.Width, bmp.Height);
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
            layouter.Reset();
            return bmp;
        }

        private float CalculateCloudScale(Tag tag, ImageSettings imageSettings)
        {
            var furthestRectanglePoint = GetFurthestRectanglePoint(imageSettings.CloudCenter, tag.TagBox);
            var cloudBorderSize = GetCloudBorderSize(imageSettings);
            var distanceToImageBorder = Geometry
                .GetLengthFromRectangleCenterToBorderOnVector(
                    new Rectangle(Point.Empty, cloudBorderSize),
                    furthestRectanglePoint
                );
            return distanceToImageBorder == 0 ? 1 : (float) (distanceToImageBorder / furthestRectanglePoint.GetLength());
        }

        private Point GetFurthestRectanglePoint(Point from, Rectangle rectangle)
        {
            var realFigureCenter = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
            var posMinusFrom = new Point(realFigureCenter.X - from.X, realFigureCenter.Y - from.Y);
            var distanceToBorder = Geometry
                .GetLengthFromRectangleCenterToBorderOnVector(
                    new Rectangle(Point.Empty, rectangle.Size),
                    posMinusFrom
                );
            var posMinusFromLength = posMinusFrom.GetLength();
            if (posMinusFromLength == 0) 
                return posMinusFrom;
            var borderScale = (distanceToBorder + posMinusFromLength) / posMinusFromLength;
            var posMinusCenterWithTagBorder = new Point(
                (int) (posMinusFrom.X * borderScale),
                (int) (posMinusFrom.Y * borderScale)
            );
            return posMinusCenterWithTagBorder;
        }

        private Size GetCloudBorderSize(ImageSettings imageSettings)
        {
            return new Size(
                Math.Min(
                    imageSettings.CloudCenter.X,
                    imageSettings.ImageSize.Width - imageSettings.CloudCenter.X
                ) * 2,
                Math.Min(
                    imageSettings.CloudCenter.Y,
                    imageSettings.ImageSize.Height - imageSettings.CloudCenter.Y
                ) * 2
            );
        }

        private static Size CalculateWordSize(WordToken wordToken, Font font, Graphics graphics)
        {
            var size = graphics.MeasureString(wordToken.Word, font);
            return size.ToSize();
        }

        private Tag CreateTag(Graphics graphics, WordToken wordToken, ImageSettings imageSettings)
        {
            var color = painter.GetTagColor();
            var fontSize = CalculateFontSize(wordToken, imageSettings);
            var wordFont = new Font(imageSettings.Font.FontFamily, fontSize, imageSettings.Font.Style);
            var wordRectangle = layouter.PutNextRectangle(CalculateWordSize(wordToken, wordFont, graphics));
            wordRectangle.Location = new Point(
                wordRectangle.X + imageSettings.CloudCenter.X,
                wordRectangle.Y + imageSettings.CloudCenter.Y
            );
            return new Tag(wordToken, wordRectangle, fontSize, color);
        }

        private void DrawTag(Graphics graphics, Tag tag, float cloudScale, ImageSettings imageSettings)
        {
            var scaledFontSize = tag.FontSize * cloudScale;
            var scaledFont = new Font(imageSettings.Font.FontFamily, scaledFontSize, imageSettings.Font.Style);
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