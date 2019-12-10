using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.Logic
{
    public class TagCloudVisualizer : IVisualizer
    {
        private readonly ILayouter layouter;
        private readonly IParser textParser;
        private readonly ITagPainter painter;
        private readonly IBoringWordsProvider boringWordsProvider;

        public TagCloudVisualizer(
            IParser textParser, 
            ILayouter layouter, 
            ITagPainter painter, 
            IBoringWordsProvider boringWordsProvider)
        {
            this.boringWordsProvider = boringWordsProvider;
            this.textParser = textParser;
            this.layouter = layouter;
            this.painter = painter;
        }

        public Bitmap VisualizeTextFromFile(string fileName, ImageSettings imageSettings)
        {
            var imageCenter = new Point(imageSettings.ImageSize.Width / 2, imageSettings.ImageSize.Height / 2);
            var text = TextRetriever.RetrieveTextFromFile(fileName);
            var wordTokens = textParser.ParseToTokens(text, boringWordsProvider.BoringWords);
            var bmp = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
            var graphics = Graphics.FromImage(bmp);
            graphics.FillRectangle(new SolidBrush(imageSettings.BackgroundColor), 0, 0, bmp.Width, bmp.Height);
            var tags = new List<Tag>();
            var cloudScale = 1f;
            foreach (var word in wordTokens)
            {
                var tag = CreateTag(graphics, word, imageSettings, imageCenter);
                var currentCloudScale = CalculateCloudScale(tag, imageSettings, imageCenter);
                if (currentCloudScale < cloudScale)
                    cloudScale = currentCloudScale;
                tags.Add(tag);
            }
            foreach (var tag in tags)
                DrawTag(graphics, tag, cloudScale, imageSettings, imageCenter);
            layouter.Reset();
            return bmp;
        }

        private float CalculateCloudScale(Tag tag, ImageSettings imageSettings, Point imageCenter)
        {
            var furthestRectanglePoint = GetFurthestRectanglePoint(imageCenter, tag.TagBox);
            var cloudBorderSize = imageSettings.ImageSize;
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

        private static Size CalculateWordSize(WordToken wordToken, Font font, Graphics graphics)
        {
            var size = graphics.MeasureString(wordToken.Word, font);
            return size.ToSize();
        }

        private Tag CreateTag(Graphics graphics, WordToken wordToken, ImageSettings imageSettings, Point imageCenter)
        {
            var color = painter.GetTagColor();
            var fontSize = CalculateFontSize(wordToken, imageSettings);
            var wordFont = new Font(imageSettings.Font.FontFamily, fontSize, imageSettings.Font.Style);
            var wordRectangle = layouter.PutNextRectangle(CalculateWordSize(wordToken, wordFont, graphics));
            wordRectangle.Location = new Point(
                wordRectangle.X + imageCenter.X,
                wordRectangle.Y + imageCenter.Y
            );
            return new Tag(wordToken, wordRectangle, fontSize, color);
        }

        private void DrawTag(Graphics graphics, Tag tag, float cloudScale, ImageSettings imageSettings, Point imageCenter)
        {
            var scaledFontSize = tag.FontSize * cloudScale;
            var scaledFont = new Font(imageSettings.Font.FontFamily, scaledFontSize, imageSettings.Font.Style);
            var positionMinusCenter = new Point(
                tag.TagBox.Location.X - imageCenter.X,
                tag.TagBox.Location.Y - imageCenter.Y
            );
            var newPointLocation = new Point(
                imageCenter.X + (int) (positionMinusCenter.X * cloudScale),
                imageCenter.Y + (int) (positionMinusCenter.Y * cloudScale)
            );
            graphics.DrawString(tag.WordToken.Word, scaledFont, new SolidBrush(tag.Color), newPointLocation);
        }

        private float CalculateFontSize(WordToken word, ImageSettings imageSettings)
        {
            return imageSettings.Font.Size + word.TextCount * 3;
        }
    }
}