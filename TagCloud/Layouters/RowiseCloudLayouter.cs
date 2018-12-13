using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;
using Extensions;
using MoreLinq;

namespace TagCloud.Layouters
{
   //Layouter Code
    
    public class RowiseCloudLayouter : ICloudLayouter
    {
        private readonly List<RowLayout> rowLayouts = new List<RowLayout>();
        private int firstIndex;
        
        public RowiseCloudLayouter(Point center)
        {
            Center = center;
        }

        public RowiseCloudLayouter(int x, int y)
        {
            Center = new Point(x, y);
        }

        public Point Center { get; }
        public IEnumerable<Rectangle> Layout => rowLayouts.SelectMany(x => x.Body);

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rowLayouts.Count == 0)
            {
                var rect = rectangleSize.WithCenterIn(Center);
                rowLayouts.Add(new RowLayout(rect));
                return rect;
            }

            if (rowLayouts.Sum(x => x.Bounds.Height) < rowLayouts.Max(x => x.Bounds.Width) ||
                rowLayouts.Max(x => x.Bounds.Height) < rectangleSize.Height)
                return AddNewRow(rectangleSize);

            return rowLayouts.Where(x => x.Bounds.Height >= rectangleSize.Height)
                            .MinBy(x => x.Bounds.Width)
                            .Add(rectangleSize);
        }

        private Rectangle AddNewRow(Size rectangleSize)
        {  
            var heights = rowLayouts.Select(x => x.Bounds.Height).ToArray();
            return heights.Take(firstIndex).Sum() > heights.Skip(firstIndex + 1).Sum() ?
                AddFirstRow(rectangleSize) : AddLastRow(rectangleSize);
        }

        private Rectangle AddLastRow(Size rectangleSize)
        {
            var height = rowLayouts.Last().Bounds.Bottom;
            var rect = new Rectangle(new Point(Center.X - rectangleSize.Width / 2, height), rectangleSize);
            rowLayouts.Add(new RowLayout(rect));
            firstIndex++;
            return rect;
        }
        
        private Rectangle AddFirstRow(Size rectangleSize)
        {
            var height = rowLayouts.First().Bounds.Top - rectangleSize.Height;
            var rect = new Rectangle(new Point(Center.X - rectangleSize.Width / 2, height), rectangleSize);
            rowLayouts.Insert(0,new RowLayout(rect));
            return rect;
        }
    }
}
