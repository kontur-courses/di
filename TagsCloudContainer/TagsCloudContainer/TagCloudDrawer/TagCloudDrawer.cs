using Autofac;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;


namespace TagsCloudContainer
{
    public class TagCloudDrawer
    {
        private readonly PictureInfo pictureInfo;
        private readonly ITagCloudBuilder tagCloudBuilder;
        private readonly ITagsLayouter tagsLayouter;
        private readonly ITagsPaintingAlgorithm painter;
        private readonly ILogger logger;

        public TagCloudDrawer(PictureInfo pictureInfo, ITagsLayouter tagsLayouter,
            ITagCloudBuilder tagCloudBuilder, ITagsPaintingAlgorithm painter, ILogger logger)
        {
            this.pictureInfo = pictureInfo;
            this.tagCloudBuilder = tagCloudBuilder;
            this.painter = painter;
            this.tagsLayouter = tagsLayouter;
            this.logger = logger;
        }


        private List<Rectangle> GetRectangles(IEnumerable<Tag> tags)
        {
            return tags.Select(x => tagsLayouter.PutNextRectangle(x.Size)).ToList();
        }

        private int GetShiftX (IEnumerable<Rectangle> rectangles) => rectangles.Min(x => x.Left);
        private int GetShiftY(IEnumerable<Rectangle> rectangles) => rectangles.Min(x => x.Top);
        private int GetWidth(IEnumerable<Rectangle> rectangles) =>
            rectangles.Max(x => x.Right) - rectangles.Min(x => x.Left);
        private int GetHeight(IEnumerable<Rectangle> rectangles) =>
            rectangles.Max(x => x.Bottom) - rectangles.Min(x => x.Top);

        public void DrawTagCloud(int maxWordsCnt)
        {
            var tags = tagCloudBuilder.GetTagsCloud().Take(maxWordsCnt).ToList();
            var rectangles = GetRectangles(tags);
            var width = GetWidth(rectangles);
            var height = GetHeight(rectangles);
            var shiftX = GetShiftX(rectangles);
            var shiftY = GetShiftY(rectangles);
            using (var image = new Bitmap(width, height))
            using (var drawingObj = Graphics.FromImage(image))
            using (var strFormat = new StringFormat { Alignment = StringAlignment.Center })
            {
                var colors = painter.GetColorForTag(tags);
                for (var ind = 0; ind < tags.Count; ind++)
                {
                    var curTag = tags[ind];
                    var curRectangle = rectangles[ind];
                    curRectangle.Location = new Point(curRectangle.Location.X - shiftX,
                        curRectangle.Location.Y - shiftY);
                    var curRectangleSize = curRectangle.Size;
                    var curColor = colors[ind];
                    using (var font = new Font("Georgia", curRectangleSize.Height / 2))
                    using (var brush = new SolidBrush(curColor))
                        drawingObj.DrawString(curTag.Word, font, brush, curRectangle, strFormat);
                }

                var imagePath = Path.Combine(new string[] { AppDomain.CurrentDomain.BaseDirectory,
                    pictureInfo.FileName + "." + pictureInfo.Format });
                logger.LogOut("Tag cloud visualization saved to file " + imagePath);
                image.Save(imagePath);
            }
        }
    }
}