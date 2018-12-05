using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            var options = new TagCloudLayoutOptions(new RoundSpiralGenerator(new Point(500, 500), 3), new Point(500, 500),3);
            var layouter = new CircularCloudLayouter(options);
            var rectangles = layouter.PutNextRectangles(GenerateRectangles(SizeSequenceCreators.Random))
                                     .ToList();
            Console.Out.WriteLine(rectangles.Count);
            rectangles.DrawRectangles(layouter.Center, expandPercent: 250, name: "random");
        }

        public static IEnumerable<Size> GenerateRectangles(SizeSequenceCreators.CreatorInfo creatorInfo)
        {
            return Enumerable.Range(creatorInfo.Start, creatorInfo.Count)
                             .Select(creatorInfo.Creator);
        }
    }
}
