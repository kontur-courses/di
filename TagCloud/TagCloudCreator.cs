using System.Collections.Generic;
using System.Drawing;
using TagCloud.PointGenerators;
using TagCloud.Tag;
using TagCloud.WordPreprocessors;

namespace TagCloud
{
    public static class TagCloudCreator
    {
        public static WordsTagCloud Create(IWordPreprocessor wordPreprocessor, IPointGenerator pointGenerator)
        {
            var circularCloudLayouter = new CircularCloudLayouter(pointGenerator);

            WordsTagCloud tagCloud = WordsTagCloud.Create(wordPreprocessor, pointGenerator);
            
            return tagCloud;
        }

        public static TagCloud Create(IEnumerable<Size> rectangleSizes, Point inCenter)
        {
            var circularCloudLayouter = new CircularCloudLayouter(new SpiralPointGenerator(inCenter));

            TagCloud tagCloud = new TagCloud(inCenter);

            foreach (var rectangleSize in rectangleSizes)
                tagCloud.Rectangles.Add(new Layout(
                    circularCloudLayouter.PutNextRectangle(rectangleSize)));

            return tagCloud;
        }
    }
}
