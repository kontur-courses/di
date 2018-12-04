using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class TagsCloudVisualization
    {
        static void Main(string[] args)
        {
            var allSizes = new List<Size>();
            var rnd = new Random();
            for (var i = 0; i < 100; i++)
            {
                var nextHeight = rnd.Next(20, 25);
                //var nextHeight = 40;
                var nextWidth = rnd.Next(nextHeight * 2, nextHeight * 6);
                //var nextWidth = 160;
                allSizes.Add(new Size(nextWidth, nextHeight));
            }

            var circularCloudLayouter = new CircularCloudLayouter(new Point(0, 5));
            foreach (var r in allSizes)
            {
                circularCloudLayouter.PutNextRectangle(r);
            }

            var render = new TagsCloudRenderer(FontFamily.GenericMonospace, Color.DodgerBlue);
            render.RenderIntoFile("img.png", circularCloudLayouter.TagsCloud);
        }
    }
}