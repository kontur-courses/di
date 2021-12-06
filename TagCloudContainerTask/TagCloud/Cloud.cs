using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Geometry;
using TagCloud.Layouting;
using TagCloud.Saving;
using TagCloud.Visualization;

namespace TagCloud
{
    public class Cloud : ICloud

    {
        private readonly IBitmapSaver bitmapSaver;
        private readonly ICloudLayouter layouter;
        private readonly IVisualizer visualizer;

        public Cloud(ICloudLayouter layouter,
            IBitmapSaver bitmapSaver,
            IVisualizer visualizer)
        {
            this.layouter = layouter;
            this.bitmapSaver = bitmapSaver;
            this.visualizer = visualizer;
        }

        public string SaveBitmap(bool shouldShowLayout = true,
            bool shouldShowMarkup = true, bool openAfterSave = true)
        {
            var canvas = Visualize(shouldShowLayout, shouldShowMarkup);
            var path = bitmapSaver.Save(canvas, openAfterSave);

            return path;
        }

        public void PutNextTag(Size tagRectangleSize)
        {
            layouter.PutNextRectangle(tagRectangleSize);
        }

        private Bitmap Visualize(bool shouldShowLayout, bool shouldShowMarkup)
        {
            var canvas = CreateCanvas();
            var rectangles = RelocateRectangles(layouter.GetRectanglesCopy(), canvas.Size);

            using (var g = Graphics.FromImage(canvas))
            {
                if (shouldShowLayout)
                    visualizer.VisualizeCloud(g, layouter.Center, rectangles);

                if (shouldShowMarkup)
                {
                    var cloudRadius = layouter.GetCloudBoundaryRadius();
                    visualizer.VisualizeDebuggingMarkup(g, canvas.Size,
                        layouter.Center.MovePointToSizeCenter(canvas.Size, true),
                        cloudRadius);
                }
            }

            return canvas;
        }

        private Bitmap CreateCanvas()
        {
            var radius = layouter.GetCloudBoundaryRadius();
            var canvasSize = new Size(radius * 2, radius * 2);

            if (canvasSize.Width < 1 || canvasSize.Height < 1)
                throw new ArgumentException("image width and height can't be lower than 1");

            return new Bitmap(canvasSize.Width, canvasSize.Height);
        }

        private static List<Rectangle> RelocateRectangles(List<Rectangle> rectangles, Size imgSize)
        {
            for (var i = 0; i < rectangles.Count; i++)
            {
                var newLocation = rectangles[i].Location.MovePointToSizeCenter(imgSize, true);

                var newRect = new Rectangle(newLocation, rectangles[i].Size);
                rectangles[i] = newRect;
            }

            return rectangles;
        }
    }
}