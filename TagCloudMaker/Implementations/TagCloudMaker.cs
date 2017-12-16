using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagCloud.Interfaces;

namespace TagCloud.Implementations
{
    public class TagCloudMaker: ITagCloudMaker
    {
        private readonly IWordProcessor wordProcessor;
        private readonly ICloudLayouter cloudLayouter;
        private readonly ITagCloudDrawer tagCloudDrawer;
        private readonly IImageSaver imageSaver;

        public TagCloudMaker(IWordProcessor wordProcessor, ICloudLayouter cloudLayouter, 
                             ITagCloudDrawer tagCloudDrawer, IImageSaver imageSaver)
        {
            this.wordProcessor = wordProcessor;
            this.cloudLayouter = cloudLayouter;
            this.tagCloudDrawer = tagCloudDrawer;
            this.imageSaver = imageSaver;
        }

        public Result<string> CreateTagCloud(string filePath, int minLetterSize, DrawingSettings settings)
        {
            var result = GetTagCloudRectangles(filePath, minLetterSize);
            if (!result.IsSuccess)
                return Result.Fail<string>(result.Error);

            var rects = TextRectangle.NormalizeRectangles(result.Value, settings.ImageSize);
            var img = DrawTagCloud(rects, settings);
            return Result.Ok(SaveTagCloud(img, settings.ImageFormat));
        }

        private Image DrawTagCloud(IEnumerable<TextRectangle> rectangles, DrawingSettings settings)
        {
            return tagCloudDrawer.DrawTagCloud(rectangles, settings);
        }

        private string SaveTagCloud(Image image, ImageFormat format)
        {
            return imageSaver.SaveImage(image, format);
        }

        private Result<TextRectangle[]> GetTagCloudRectangles(string filePath, int minLetterSize)
        {
            var result = wordProcessor.GetFrequencyDictionary(filePath);
            if (!result.IsSuccess)
                return Result.Fail<TextRectangle[]>(result.Error);

            var frecDict = result.Value;
            var min = frecDict.Select(p => p.Value).Min();
            foreach (var pair in frecDict.OrderByDescending(p => p.Value))
            {
                var fontCoeff = pair.Value / min;
                var size = new Size(minLetterSize * pair.Key.Length * fontCoeff, minLetterSize * fontCoeff);

                var putRectangleResult = cloudLayouter.PutNextRectangle(size, pair.Key);
                if (!putRectangleResult.IsSuccess)
                    return Result.Fail<TextRectangle[]>(putRectangleResult.Error);
            }
            return Result.Ok(cloudLayouter.CloudRectangles);
        }
    }
}