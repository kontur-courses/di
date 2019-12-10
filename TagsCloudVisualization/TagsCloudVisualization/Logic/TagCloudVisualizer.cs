using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.Logic
{
    public class TagCloudVisualizer : IVisualizer
    {
        private readonly ILayouter layouter;
        private readonly IParser textParser;
        private readonly IImageGenerator imageGenerator;
        private readonly ITagPainter painter;
        private readonly IBoringWordsProvider boringWordsProvider;
        private readonly Graphics measureGraphics;

        public TagCloudVisualizer(
            IImageGenerator imageGenerator,
            IParser textParser, 
            ILayouter layouter, 
            ITagPainter painter, 
            IBoringWordsProvider boringWordsProvider)
        {
            measureGraphics = Graphics.FromImage(new Bitmap(1, 1));
            this.imageGenerator = imageGenerator;
            this.boringWordsProvider = boringWordsProvider;
            this.textParser = textParser;
            this.layouter = layouter;
            this.painter = painter;
        }

        public Bitmap VisualizeTextFromFile(string fileName, ImageSettings imageSettings)
        {
            var imageCenter = new Point(imageSettings.ImageSize.Width / 2, imageSettings.ImageSize.Height / 2);
            var text = TextRetriever.RetrieveTextFromFile(fileName);
            var tags = textParser
                .ParseToTokens(text, boringWordsProvider.BoringWords)
                .Select(token => CreateTag(token, imageSettings, imageCenter))
                .ToArray();
            var cloudScale = CalculateCloudScale(tags, imageSettings, imageCenter);
            layouter.Reset();
            return imageGenerator.CreateImage(tags, cloudScale, imageSettings);
        }

        private float CalculateCloudScale(IEnumerable<Tag> tags, ImageSettings imageSettings, Point imageCenter)
        {
            var cloudScale = 1f;
            foreach (var tag in tags)
            {
                var currentCloudScale = CalculateTagDistanceFromPointScale(tag, imageSettings, imageCenter);
                if (currentCloudScale < cloudScale)
                    cloudScale = currentCloudScale;
            }
            return cloudScale;
        }


        private float CalculateTagDistanceFromPointScale(Tag tag, ImageSettings imageSettings, Point imageCenter)
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

        private Size CalculateWordSize(WordToken wordToken, Font font)
        {
            var size = measureGraphics.MeasureString(wordToken.Word, font);
            return size.ToSize();
        }

        private Tag CreateTag(WordToken wordToken, ImageSettings imageSettings, Point imageCenter)
        {
            var color = painter.GetTagColor();
            var fontSize = CalculateFontSize(wordToken, imageSettings);
            var wordFont = new Font(imageSettings.Font.FontFamily, fontSize, imageSettings.Font.Style);
            var wordRectangle = layouter.PutNextRectangle(CalculateWordSize(wordToken, wordFont));
            wordRectangle.Location = new Point(
                wordRectangle.X + imageCenter.X,
                wordRectangle.Y + imageCenter.Y
            );
            return new Tag(wordToken, wordRectangle, fontSize, color);
        }

        private float CalculateFontSize(WordToken word, ImageSettings imageSettings)
        {
            return imageSettings.Font.Size + word.TextCount * 3;
        }
    }
}