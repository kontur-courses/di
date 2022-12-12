using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public IList<ICloudItem> Items { get; }
        public IDistribution Distribution { get; }

        public CircularCloudLayouter(IDistribution distribution)
        {
            Items = new List<ICloudItem>();
            Distribution = distribution;
        }

        public ICloudItem PutNextCloudItem(string word, Size size, Font font)
        {
            if (size.Width < 0 || size.Height < 0)
                throw new ArgumentException("Height and width must be positive");

            foreach (var point in Distribution.GetPoints())
            {
                var location = new Point(new Size(point) - (size / 2));
                var rectangle = new Rectangle(location, size);

                if (Items.All(item => !item.Rectangle.IntersectsWith(rectangle)))
                {
                    var item = new CloudItem(word, rectangle, font);
                    Items.Add(item);
                    return item;
                }
            }

            throw new Exception();
        }
    }
}