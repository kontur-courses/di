using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.CircularCloudLayouter;


namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var value in new List<double>() { 377 })
            {
                var layout = new CircularCloudLayouter.CircularCloudLayouter(
                    new RectangleStorage(new Point(), new Direction(value)));

                var rnd = new Random();
                for (var i = 0; i < rnd.Next(1000, 1001); i++)
                {
                    var y = 40;
                    var x = 7 * y;


                    layout.PutNextRectangle(new Size(x, y));
                }

                var visualizer = new Visualizer(layout);
                visualizer.DrawTagsCloud("..\\..\\DirectionTest\\Direct " + value + " A.png");
            }

        }
    }
}
