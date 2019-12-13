using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;


namespace TagsCloudContainer
{
    public class TagCloudDrawer
    {
        private readonly PictureInfo pictureInfo;
        private readonly TagCloudBuilder tagCloudBuilder;
        private readonly CircularCloudLayouter tagCloud;
        private readonly IPaintingAlgorithm painter;


        public TagCloudDrawer(PictureInfo pictureInfo, TagCloudBuilder tagCloudBuilder, IPaintingAlgorithm painter)
        {
            this.pictureInfo = pictureInfo;
            this.tagCloudBuilder = tagCloudBuilder;
            this.painter = painter;
            tagCloud = new CircularCloudLayouter(pictureInfo.TagCloudCenter);
        }


        private List<Rectangle> GetRectangles(IEnumerable<Tag> tags)
        {
            return tags.Select(x => tagCloud.PutNextRectangle(x.Size)).ToList();
        }

        public void DrawTagCloud(string fileName, int maxWordsCnt)
        {
            var tags = tagCloudBuilder.GetTagClouds(fileName).Take(maxWordsCnt).ToList();
            var rectangles = GetRectangles(tags);
            var maxY = rectangles.Max(x => x.Bottom);
            var minY = rectangles.Min(x => x.Top);
            var width = rectangles.Max(x => x.Right) - rectangles.Min(x => x.Left);
            var height = rectangles.Max(x => x.Bottom) - rectangles.Min(x => x.Top);
            var shiftX = rectangles.Min(x => x.Left);
            var shiftY = rectangles.Min(x => x.Top);
            var image = new Bitmap(width, height);
            var drawingObj = Graphics.FromImage(image);
            var strFormat = new StringFormat {Alignment = StringAlignment.Center};
            var colors = painter.GetColorForTag(tags);
            for (var ind = 0; ind < tags.Count; ind++)
            {
                var curTag = tags[ind];
                var curRectangle = rectangles[ind];
                curRectangle.Location = new Point(curRectangle.Location.X - shiftX,
                    curRectangle.Location.Y - shiftY);
                var curRectangleSize = curRectangle.Size;
                var curColor = colors[ind++];
                drawingObj.DrawString(curTag.Word, new Font("Georgia", curRectangleSize.Height / 2),
                    new SolidBrush(curColor), curRectangle, strFormat);
            }

            var imagePath = Path.Combine(new string[] { AppDomain.CurrentDomain.BaseDirectory,
                pictureInfo.FileName + ".png" });
            OutputLogger.AddLog("Tag cloud visualization saved to file " + imagePath);
            image.Save(imagePath);
            drawingObj.Dispose();
            image.Dispose();
            strFormat.Dispose();
        }
    }
}