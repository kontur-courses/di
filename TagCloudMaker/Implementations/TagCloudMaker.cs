using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TagCloud
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

        public IEnumerable<TextRectangle> CreateTagCloud(IEnumerable<string> words, int minLetterSize, string pathToSave)
        {
            return GetTagCloudRectangles(words, minLetterSize);
        }

        public Image DrawTagCloud(IEnumerable<TextRectangle> rectangles, DrawingSettings settings)
        {
            return tagCloudDrawer.DrawTagCloud(rectangles, settings);
        }

        public string SaveTagCloud(Image image, ImageFormat format)
        {
            return imageSaver.SaveImage(image, format);
        }

        private IEnumerable<TextRectangle> GetTagCloudRectangles(IEnumerable<string> words, int minLetterSize)
        {
            var frecDict = wordProcessor.GetFrequencyDictionary(words);
            var tenPercent = frecDict.Select(p => p.Value).Max() / 10;
            foreach (var pair in frecDict)
            {
                var fontCoeff = Math.Max(1, pair.Value / tenPercent);
                var size = new Size(minLetterSize * fontCoeff, minLetterSize * pair.Key.Length * fontCoeff);
                cloudLayouter.PutNextRectangle(size, pair.Key);
            }
            return cloudLayouter.CloudRectangles;
        }
    }
}