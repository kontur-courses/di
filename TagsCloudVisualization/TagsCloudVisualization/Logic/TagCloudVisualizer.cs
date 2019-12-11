using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Logic.Painter;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.Logic
{
    public class TagCloudVisualizer : IVisualizer
    {
        private readonly ILayouter layouter;
        private readonly IParser textParser;
        private readonly IImageGenerator imageGenerator;
        private readonly ITagPainter painter;
        private readonly IImageSettingsProvider imageSettingsProvider;
        private readonly Graphics measureGraphics;

        public TagCloudVisualizer(
            IImageGenerator imageGenerator,
            IParser textParser, 
            ILayouter layouter, 
            ITagPainter painter,
            IImageSettingsProvider imageSettingsProvider)
        {
            measureGraphics = Graphics.FromImage(new Bitmap(1, 1));
            this.imageGenerator = imageGenerator;
            this.imageSettingsProvider = imageSettingsProvider;
            this.textParser = textParser;
            this.layouter = layouter;
            this.painter = painter;
        }

        public Bitmap VisualizeTextFromFile(string fileName)
        {
            var imageCenter = new Point(
                imageSettingsProvider.ImageSettings.ImageSize.Width / 2, 
                imageSettingsProvider.ImageSettings.ImageSize.Height / 2
                );
            var text = TextRetriever.RetrieveTextFromFile(fileName);
            var tags = textParser
                .ParseToTokens(text)
                .Select(token => CreateTag(token,imageCenter))
                .ToArray();
            var cloudScale = CalculateCloudScale(tags,imageCenter);
            layouter.Reset();
            return imageGenerator.CreateImage(tags,cloudScale, imageSettingsProvider.ImageSettings);
        } 

        private float CalculateCloudScale(IEnumerable<Tag> tags, Point imageCenter)
        {
            var cloudScale = 1f;
            foreach (var tag in tags)
            {
                var currentCloudScale = CalculateTagDistanceFromPointScale(tag, imageCenter);
                if (currentCloudScale < cloudScale)
                    cloudScale = currentCloudScale;
            }
            return cloudScale;
        }


        private float CalculateTagDistanceFromPointScale(Tag tag, Point imageCenter)
        {
            var furthestRectanglePoint = GetFurthestRectanglePoint(imageCenter, tag.TagBox);
            var cloudBorderSize = imageSettingsProvider.ImageSettings.ImageSize;
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

        private Tag CreateTag(WordToken wordToken, Point imageCenter)
        {
            var color = painter.GetTagColor();
            var fontSize = CalculateFontSize(wordToken, imageSettingsProvider.ImageSettings);
            var wordFont = new Font(
                imageSettingsProvider.ImageSettings.Font.FontFamily, 
                fontSize, 
                imageSettingsProvider.ImageSettings.Font.Style
                );
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