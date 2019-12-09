using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CircularCloudLayouter
{

    public class CircularCloudLayouter : ICircularCloudLayouter
    {

        public delegate CircularCloudLayouter Factory(Point center);

        private readonly Point CloudCenter;
        private readonly HashSet<Segment> BorderSegments;
        private bool isFirstRectangle;
        private readonly List<Rectangle> AddedRectangles;
        private int xCompression;
        private int yCompression;
        public CircularCloudLayouter(Point center)
        {
            CloudCenter = center;
            BorderSegments = new HashSet<Segment>();
            isFirstRectangle = true;
            AddedRectangles = new List<Rectangle>();
            xCompression = 1;
            yCompression = 1;
        }

        public CircularCloudLayouter()
        {
            CloudCenter = new Point(100, 100);
            BorderSegments = new HashSet<Segment>();
            isFirstRectangle = true;
            AddedRectangles = new List<Rectangle>();
            xCompression = 1;
            yCompression = 1;
        }

        public CircularCloudLayouter(Point center, int xCompression, int yCompression)
        {
            CloudCenter = center;
            BorderSegments = new HashSet<Segment>();
            isFirstRectangle = true;
            AddedRectangles = new List<Rectangle>();
            this.xCompression = xCompression;
            this.yCompression = yCompression;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (isFirstRectangle)
            {
                isFirstRectangle = false;
                InitializeFirstRectangle(rectangleSize);
                var firstRect = new Rectangle(new Point(CloudCenter.X - (int)Math.Floor(rectangleSize.Width / (double)2), CloudCenter.Y - (int)Math.Floor(rectangleSize.Height / (double)2)), rectangleSize);
                return firstRect;
            }
            var searchResult = new PositionSearchResult(double.MaxValue, null, new Point());
            foreach (var segment in BorderSegments)
            {
                if (segment.Horizontal())
                {
                    var leftBorderX = FindBorder(segment, rectangleSize, Segment.Type.Left);
                    var rightBorderX = FindBorder(segment, rectangleSize, Segment.Type.Right);
                    var extendedSegment = new Segment(leftBorderX, segment.Start.Y, rightBorderX, segment.End.Y, segment.SegmentType);
                    searchResult = SearchMinDistance(searchResult, segment, rectangleSize, extendedSegment);
                }
                else
                {
                    var topBorderY = FindBorder(segment, rectangleSize, Segment.Type.Top);
                    var bottomBorderY = FindBorder(segment, rectangleSize, Segment.Type.Bottom);
                    var extendedSegment = new Segment(segment.Start.X, topBorderY, segment.End.X, bottomBorderY, segment.SegmentType);
                    searchResult = SearchMinDistance(searchResult, segment, rectangleSize, extendedSegment);
                }
            }
            var outRectangle = new Rectangle(searchResult.ClosestRectCoordinates, rectangleSize);
            SegmentStacker.StackSegments(Segment.GetSegmentsFromRectangle(outRectangle), BorderSegments);
            AddedRectangles.Add(outRectangle);
            return outRectangle;
        }


        private PositionSearchResult SearchMinDistance(PositionSearchResult searchResult, Segment segment, Size rectangleSize, Segment extendedSegment)
        {
            if (segment.Horizontal())
            {
                if (extendedSegment.Length < rectangleSize.Width)
                    return searchResult;

                if (extendedSegment.Start.X < CloudCenter.X
                && extendedSegment.End.X > CloudCenter.X
                && CloudCenter.X + (int)Math.Truncate(rectangleSize.Width / (double)2) + 1 <= extendedSegment.End.X
                && CloudCenter.X - (int)Math.Truncate(rectangleSize.Width / (double)2) - 1 >= extendedSegment.Start.X)
                {
                    Point midRectCoordinates;
                    if (segment.SegmentType == Segment.Type.Top)
                        midRectCoordinates = new Point(CloudCenter.X - (int)Math.Truncate(rectangleSize.Width / (double)2), extendedSegment.Start.Y - rectangleSize.Height);
                    else
                        midRectCoordinates = new Point(CloudCenter.X - (int)Math.Truncate(rectangleSize.Width / (double)2), extendedSegment.Start.Y);
                    if (CheckOppositeBorder(midRectCoordinates, rectangleSize))
                        searchResult = CheckDistance(searchResult, segment, midRectCoordinates, rectangleSize);
                }
                Point leftMostRectCoordinates;
                Point rightMostRectCoordinates;
                if (segment.SegmentType == Segment.Type.Top)
                {
                    leftMostRectCoordinates = new Point(extendedSegment.Start.X, extendedSegment.Start.Y - rectangleSize.Height);
                    rightMostRectCoordinates = new Point(extendedSegment.End.X - rectangleSize.Width, extendedSegment.Start.Y - rectangleSize.Height);
                }
                else
                {
                    leftMostRectCoordinates = new Point(extendedSegment.Start.X, extendedSegment.Start.Y);
                    rightMostRectCoordinates = new Point(extendedSegment.End.X - rectangleSize.Width, extendedSegment.Start.Y);
                }
                if (CheckOppositeBorder(leftMostRectCoordinates, rectangleSize))
                    searchResult = CheckDistance(searchResult, segment, leftMostRectCoordinates, rectangleSize);
                if (CheckOppositeBorder(rightMostRectCoordinates, rectangleSize))
                    searchResult = CheckDistance(searchResult, segment, rightMostRectCoordinates, rectangleSize);
            }
            else
            {
                if (extendedSegment.Length < rectangleSize.Height)
                    return searchResult;

                if (extendedSegment.Start.Y < CloudCenter.Y
                && extendedSegment.End.Y > CloudCenter.Y
                && CloudCenter.Y + (int)Math.Truncate(rectangleSize.Height / (double)2) + 1 <= extendedSegment.End.Y
                && CloudCenter.Y - (int)Math.Truncate(rectangleSize.Height / (double)2) - 1 >= extendedSegment.Start.Y)
                {
                    Point midRectCoordinates;
                    if (segment.SegmentType == Segment.Type.Left)
                        midRectCoordinates = new Point(extendedSegment.Start.X - rectangleSize.Width, CloudCenter.Y - (int)Math.Truncate(rectangleSize.Height / (double)2));
                    else
                        midRectCoordinates = new Point(extendedSegment.Start.X, CloudCenter.Y - (int)Math.Truncate(rectangleSize.Height / (double)2));
                    if (CheckOppositeBorder(midRectCoordinates, rectangleSize))
                        searchResult = CheckDistance(searchResult, segment, midRectCoordinates, rectangleSize);
                }
                Point topMostRectCoordinates;
                Point botMostRectCoordinates;
                if (segment.SegmentType == Segment.Type.Left)
                {
                    topMostRectCoordinates = new Point(extendedSegment.Start.X - rectangleSize.Width, extendedSegment.Start.Y);
                    botMostRectCoordinates = new Point(extendedSegment.End.X - rectangleSize.Width, extendedSegment.End.Y - rectangleSize.Height);
                }
                else
                {
                    topMostRectCoordinates = new Point(extendedSegment.Start.X, extendedSegment.Start.Y);
                    botMostRectCoordinates = new Point(extendedSegment.End.X, extendedSegment.End.Y - rectangleSize.Height);
                }
                if (CheckOppositeBorder(topMostRectCoordinates, rectangleSize))
                    searchResult = CheckDistance(searchResult, segment, topMostRectCoordinates, rectangleSize);
                if (CheckOppositeBorder(botMostRectCoordinates, rectangleSize))
                    searchResult = CheckDistance(searchResult, segment, botMostRectCoordinates, rectangleSize);
            }
            return searchResult;

        }


        private bool CheckOppositeBorder(Point rectanglePos, Size rectangleSize)
        {
            var rectangle = new Rectangle(rectanglePos, rectangleSize);
            var intersects = BorderSegments.Where(x => rectangle.IsIntersected(x));
            return !intersects.Any();
        }


        private int FindBorder(Segment segment, Size rectangleSize, Segment.Type borderType)
        {
            if (!Segment.Horizontal(borderType))
            {
                var topRectSide = segment.SegmentType == Segment.Type.Top ? segment.End.Y - rectangleSize.Height : segment.End.Y;
                var bottomRectSide = topRectSide + rectangleSize.Height;
                var side = new Segment(0, topRectSide, 0, bottomRectSide, borderType);
                Segment border;
                if (borderType == Segment.Type.Left)
                    border = BorderSegments
                                .Where(x =>
                                x.SegmentType == Segment.OppositeType(borderType)
                                && x.Start.X <= segment.Start.X
                                && x.Intersects(side))
                                .OrderByDescending(x => x.Start.X)
                                .FirstOrDefault();
                else
                    border = BorderSegments
                            .Where(x =>
                            x.SegmentType == Segment.OppositeType(borderType)
                            && x.Start.X >= segment.End.X
                            && x.Intersects(side))
                            .OrderBy(x => x.Start.X)
                            .FirstOrDefault();
                return (border == null) ? (borderType == Segment.Type.Left ? -100000 : 100000) : border.Start.X;
            }
            else
            {
                int leftRectSide = segment.SegmentType == Segment.Type.Left ? segment.End.X - rectangleSize.Width : segment.End.X;
                int rightRectSide = leftRectSide + rectangleSize.Width;
                var side = new Segment(leftRectSide, 0, rightRectSide, 0, borderType);
                Segment border;
                if (borderType == Segment.Type.Top)
                    border = BorderSegments
                            .Where(x =>
                            x.SegmentType == Segment.OppositeType(borderType)
                            && x.Start.Y <= segment.Start.Y
                            && x.Intersects(side))
                            .OrderByDescending(x => x.Start.Y)
                            .FirstOrDefault();
                else
                    border = BorderSegments
                        .Where(x =>
                        x.SegmentType == Segment.OppositeType(borderType)
                        && x.End.Y >= segment.End.Y
                        && x.Intersects(side))
                        .OrderBy(x => x.Start.Y)
                        .FirstOrDefault();
                return (border == null) ? (borderType == Segment.Type.Top ? -100000 : 100000) : border.Start.Y;
            }
        }


        private PositionSearchResult CheckDistance(PositionSearchResult currentSearchRes, Segment segment, Point rectangleCoord, Size rectangleSize)
        {
            var rectangle = new Rectangle(rectangleCoord, rectangleSize);
            var dist = rectangle.GetRectangleCenter().DistanceWithCompression(CloudCenter, xCompression, yCompression);
            if (dist < currentSearchRes.MinDistance)
                return currentSearchRes.Update(dist, segment, rectangleCoord);

            return currentSearchRes;
        }


        private void InitializeFirstRectangle(Size rectangleSize)
        {
            var firstRectCoordinate = new Point(
                    CloudCenter.X - (int)Math.Floor(rectangleSize.Width / (double)2)
                    , CloudCenter.Y - (int)Math.Floor(rectangleSize.Height / (double)2));
            var firstRect = new Rectangle(firstRectCoordinate, rectangleSize);
            BorderSegments.UnionWith(Segment.GetSegmentsFromRectangle(firstRect));
            AddedRectangles.Add(firstRect);
        }

        public List<Rectangle> GetRectangles()
        {
            return AddedRectangles;
        }

        public void SetCompression(int xCompression, int yCompression)
        {
            this.xCompression = xCompression;
            this.yCompression = yCompression;
        }
    }

}
