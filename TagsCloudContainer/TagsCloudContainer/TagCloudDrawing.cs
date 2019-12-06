using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace TagsCloudContainer
{
    public class TagCloudDrawing
    {
        private readonly int imageWidth;
        private readonly int imageHeight;
        private readonly string fileName;
        private readonly CircularCloudLayouter tagCloud =
            new CircularCloudLayouter(new Point(1, 2));

        public TagCloudDrawing(int imageWidth, int imageHeight, string fileName,
            Point tagCloudCenter)
        {
            if (imageHeight <= 0 || imageWidth <= 0)
                throw new ArgumentException("Size of image should be positive");
            if (fileName.Length == 0)
                throw new ArgumentException("File name must be not empty");
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;
            this.fileName = fileName + ".png";
            tagCloudCenter = new Point(tagCloudCenter.X + imageWidth / 2,
                tagCloudCenter.Y + imageHeight / 2);
            if (0 > tagCloudCenter.X || tagCloudCenter.X > imageWidth ||
                0 > tagCloudCenter.Y || tagCloudCenter.Y > imageHeight)
                throw new ArgumentException("Center of tag cloud must be in the picture");
            tagCloud = new CircularCloudLayouter(tagCloudCenter);
        }


        public void DrawTagCloud(IEnumerable<Tuple<string, Size>> sizes)
        {
            var image = new Bitmap(imageWidth, imageHeight);
            var drawingObj = Graphics.FromImage(image);
            var strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            var rnd = new Random();
            foreach (var curTag in sizes)
            {
                var curRectangleSize = curTag.Item2;
                var curRectangle = tagCloud.PutNextRectangle(curRectangleSize);
                var curWord = curTag.Item1;
                var curColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                drawingObj.DrawString(curWord, new Font("Georgia", curRectangleSize.Height / 2),
                    new SolidBrush(curColor), curRectangle, strFormat);
            }
            var imagePath = Path.Combine(new string[] { AppDomain.CurrentDomain.BaseDirectory, fileName });
            Console.WriteLine("Tag cloud visualization saved to file " + imagePath);
            image.Save(imagePath);
        }
    }
}