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
        private readonly IMystemShell mystemShell;
        private readonly IWordProcessor wordProcessor;
        private readonly ICloudLayouter cloudLayouter;
        private readonly ITagCloudDrawer tagCloudDrawer;
        private readonly IImageSaver imageSaver;

        public TagCloudMaker(IMystemShell mystemShell, IWordProcessor wordProcessor, ICloudLayouter cloudLayouter, 
                             ITagCloudDrawer tagCloudDrawer, IImageSaver imageSaver)
        {
            this.mystemShell = mystemShell;
            this.wordProcessor = wordProcessor;
            this.cloudLayouter = cloudLayouter;
            this.tagCloudDrawer = tagCloudDrawer;
            this.imageSaver = imageSaver;
        }

        public string CreateTagCloud(string filePath, int minLetterSize, DrawingSettings settings)
        {
            var rects = TextRectangle.NormalizeRectangles(GetTagCloudRectangles(filePath, minLetterSize), settings.ImageSize);
            var img = DrawTagCloud(rects, settings);
            return SaveTagCloud(img, settings.ImageFormat);
        }

        private Image DrawTagCloud(IEnumerable<TextRectangle> rectangles, DrawingSettings settings)
        {
            return tagCloudDrawer.DrawTagCloud(rectangles, settings);
        }

        private string SaveTagCloud(Image image, ImageFormat format)
        {
            return imageSaver.SaveImage(image, format);
        }

        private IEnumerable<TextRectangle> GetTagCloudRectangles(string filePath, int minLetterSize)
        {
            var frecDict = wordProcessor.GetFrequencyDictionary(mystemShell.Analyze(filePath));
            var maxToMin = Math.Max(1, frecDict.Select(p => p.Value).Max() / frecDict.Select(p => p.Value).Min());
            foreach (var pair in frecDict)
            {
                var fontCoeff = Math.Max(1, pair.Value / maxToMin);
                var size = new Size(minLetterSize * pair.Key.Length * fontCoeff, minLetterSize * fontCoeff);
                cloudLayouter.PutNextRectangle(size, pair.Key);
            }
            return cloudLayouter.CloudRectangles;
        }
    }
}