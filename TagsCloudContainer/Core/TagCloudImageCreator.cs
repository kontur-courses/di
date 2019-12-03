using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagsCloudContainer.Core
{
    class TagCloudImageCreator
    {
        private readonly CircularCloudLayouter circularCloudLayouter;
        private readonly Brush brush;
        private readonly Pen pen;
        private readonly string directory;

        public TagCloudImageCreator(CircularCloudLayouter circularCloudLayouter)
        {
            this.circularCloudLayouter = circularCloudLayouter;
            brush = new SolidBrush(Color.White);
            pen = new Pen(Color.Blue);
            directory = @"C:\\TagCloud";
        }

        public void Save(string filename = "tmp.jpg")
        {
            if (!filename.EndsWith(".jpg")) filename = $"{filename}.jpg";

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var imageSize = circularCloudLayouter.GetLayoutSize();

            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(brush, new Rectangle(0, 0, imageSize.Width, imageSize.Height));

            foreach (var rectangle in circularCloudLayouter.Rectangles)
                graphics.DrawRectangle(pen, rectangle);

            var path = Path.Combine(directory, filename);
            bitmap.Save(path, ImageFormat.Jpeg);

            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }
    }
}