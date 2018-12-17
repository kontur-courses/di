using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.TagsClouds;
using TagsCloudContainer.Visualisation.Coloring;

namespace TagsCloudContainer.Visualisation
{
    public class PngTagsCloudRenderer : ITagsCloudRenderer
    {
        private readonly Size boundary = new Size(100, 100);


        public void RenderIntoFile(ImageSettings imageSettings, IColorManager colorManager, ITagsCloud tagsCloud)
        {
            if (imageSettings.AutoSize)
            {
                RenderIntoFileAutoSize(imageSettings, colorManager, tagsCloud);
                return;
            }

            var wordsColors = colorManager.GetWordsColors(tagsCloud.AddedWords.ToList());


            var btm = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
            using (var obj = Graphics.FromImage(btm))
            {
                foreach (var tagsCloudWord in tagsCloud.AddedWords)
                {
                    DrawWord(obj, tagsCloudWord, wordsColors[tagsCloudWord], FontFamily.GenericMonospace);
                }

                btm.Save(imageSettings.OutputPath);
            }
        }

        private void RenderIntoFileAutoSize(ImageSettings imageSettings, IColorManager colorManager,
            ITagsCloud tagsCloud)
        {
            var words = tagsCloud.AddedWords.Select(x => x.Word).ToList();
            var shiftedRectangles =
                ShiftRectanglesToMainQuarter(tagsCloud.AddedWords.Select(x => x.Rectangle).ToList());
            var tagsCloudWords = words.Zip(shiftedRectangles, (word, rectangle) => (new TagsCloudWord(word, rectangle)))
                .ToList();
            var tagsCloudToDraw = new TagsCloud(tagsCloudWords);
            var wordsColors = colorManager.GetWordsColors(tagsCloudToDraw.AddedWords.ToList());

            var pictureSize = GetPictureSize(tagsCloudToDraw);

            var btm = new Bitmap(pictureSize.Width, pictureSize.Height);
            using (var obj = Graphics.FromImage(btm))
            {
                foreach (var tagsCloudWord in tagsCloudToDraw.AddedWords)
                {
                    DrawWord(obj, tagsCloudWord, wordsColors[tagsCloudWord], FontFamily.GenericMonospace);
                }

                btm.Save(imageSettings.OutputPath);
            }
        }

        private void DrawWord(Graphics graphics, TagsCloudWord tagsCloudWord, Color color, FontFamily fontFamily)
        {
            var rectangle = tagsCloudWord.Rectangle;
            var fontSize = rectangle.Height;
            graphics.DrawString(tagsCloudWord.Word, new Font(fontFamily, fontSize),
                new SolidBrush(color),
                new PointF(rectangle.X - fontSize / 4, rectangle.Y - fontSize / 4));
        }


        private Size GetPictureSize(ITagsCloud tagsCloud)
        {
            var rectangles = tagsCloud.AddedWords.Select(x => x.Rectangle).ToList();
            var maxX = rectangles.Max(x => x.Right);
            var minX = rectangles.Min(x => x.X);
            var maxY = rectangles.Max(x => x.Top);
            var minY = rectangles.Min(x => x.Bottom);
            if (minY < 0 || minX < 0)
            {
                throw new ArgumentException("Rectangles must have positive coordinates");
            }

            return new Size(maxX - minX + Math.Abs(minX * 2),
                maxY - minY + Math.Abs(minY * 2));
        }

        private IEnumerable<Rectangle> ShiftRectanglesToMainQuarter(IReadOnlyCollection<Rectangle> rectangles)
        {
            var minX = rectangles.Min(x => x.X);
            var minY = rectangles.Min(x => x.Bottom);
            var shiftX = 0;
            var shiftY = 0;
            if (minX < 0)
                shiftX = minX * -1 + boundary.Width;
            if (minY < 0)
                shiftY = minY * -1 + boundary.Height;

            return rectangles.Select(x => new Rectangle(x.X + shiftX, x.Y + shiftY, x.Width, x.Height)).ToList();
        }
    }
}