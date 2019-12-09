using System.Collections.Generic;
using System.Linq;

namespace CircularCloudLayouter
{
    public static class SegmentStacker
    {
        public static void StackSegments(HashSet<Segment> addedSegments, HashSet<Segment> borderSegments)
        {
            foreach (var segment in addedSegments)
            {
                if (segment.Horizontal())
                {
                    List<Segment> parallelSegments = borderSegments
                        .Where(x => x.Start.Y == segment.Start.Y && x.Horizontal()).ToList();
                    parallelSegments.Add(segment);
                    var stacked = RemoveIntersections(parallelSegments, segment.Start.Y, segment.SegmentType);
                    borderSegments.ExceptWith(parallelSegments);
                    borderSegments.UnionWith(stacked);
                }
                else
                {
                    List<Segment> parallelSegments = borderSegments
                        .Where(x => x.Start.X == segment.Start.X && !x.Horizontal()).ToList();
                    parallelSegments.Add(segment);
                    var stacked = RemoveIntersections(parallelSegments, segment.Start.X, segment.SegmentType);
                    borderSegments.ExceptWith(parallelSegments);
                    borderSegments.UnionWith(stacked);
                }
            }
        }


        private static List<Segment> RemoveIntersections(List<Segment> segments, int coord, Segment.Type type)
        {
            List<SegmentPoint> points = new List<SegmentPoint>();
            List<Segment> outSegments = new List<Segment>();
            foreach (var segment in segments)
            {
                if (Segment.Horizontal(type))
                {
                    points.Add(new SegmentPoint(segment.Start.X, segment.SegmentType, SegmentPoint.PointType.Start));
                    points.Add(new SegmentPoint(segment.End.X, segment.SegmentType, SegmentPoint.PointType.End));
                }
                else
                {
                    points.Add(new SegmentPoint(segment.Start.Y, segment.SegmentType, SegmentPoint.PointType.Start));
                    points.Add(new SegmentPoint(segment.End.Y, segment.SegmentType, SegmentPoint.PointType.End));
                }
            }
            Segment.Type currentType = points[0].SegmentType;
            var currentPoint = points[0];
            var startFound = true;
            bool isIntersection = false;
            bool isFirst = true;
            int lastAdded = int.MinValue;
            var sortedPoints = points.OrderBy(x => x.Coordinates).ToList();
            for (int i = 0; i < sortedPoints.Count - 1; i++)
            {
                if (sortedPoints[i].Coordinates == sortedPoints[i + 1].Coordinates
                    && sortedPoints[i].SegmentType == sortedPoints[i + 1].SegmentType)
                {
                    sortedPoints[i].Type = SegmentPoint.PointType.Start;
                    sortedPoints[i + 1].Type = SegmentPoint.PointType.Start;
                    /*удаление из середины листа слишком долгая операция, сделаем точку стартовой
                     * и она не будет рассматриваться в автомате
                     */
                }
            }
            foreach (var point in sortedPoints)
            {
                if (isFirst)
                {
                    currentType = point.SegmentType;
                    currentPoint = point;
                    startFound = true;
                    isFirst = false;
                    continue;
                }
                if (isIntersection)
                {
                    startFound = true;
                    currentPoint = point;
                    currentType = Segment.OppositeType(point.SegmentType);
                    isIntersection = false;
                    continue;
                }
                if (!startFound && point.Type == SegmentPoint.PointType.Start)
                {
                    if (lastAdded == point.Coordinates && outSegments.Last().SegmentType == point.SegmentType)
                    {
                        var lastSegment = outSegments.Last();
                        outSegments.RemoveAt(outSegments.Count - 1);
                        startFound = true;
                        if (Segment.Horizontal(type))
                            currentPoint = new SegmentPoint(lastSegment.Start.X, point.SegmentType, SegmentPoint.PointType.Start);
                        else
                            currentPoint = new SegmentPoint(lastSegment.Start.Y, point.SegmentType, SegmentPoint.PointType.Start);
                        continue;
                    }
                    currentPoint = point;
                    startFound = true;
                    currentType = point.SegmentType;
                    continue;
                }
                if (startFound && point.SegmentType == currentType && point.Type == SegmentPoint.PointType.End)
                {
                    if (Segment.Horizontal(type))
                        outSegments.Add(new Segment(currentPoint.Coordinates, coord, point.Coordinates, coord, currentType));
                    else
                        outSegments.Add(new Segment(coord, currentPoint.Coordinates, coord, point.Coordinates, currentType));
                    lastAdded = point.Coordinates;
                    startFound = false;
                    continue;
                }
                if (startFound && point.SegmentType != currentType)
                {
                    if (Segment.Horizontal(type))
                        outSegments.Add(new Segment(currentPoint.Coordinates, coord, point.Coordinates, coord, currentType));
                    else
                        outSegments.Add(new Segment(coord, currentPoint.Coordinates, coord, point.Coordinates, currentType));
                    lastAdded = point.Coordinates;
                    startFound = false;
                    isIntersection = true;
                    continue;
                }
            }
            return outSegments.Where(x => x.Length > 0).ToList();
        }

        private class SegmentPoint
        {
            public int Coordinates;
            public Segment.Type SegmentType;
            public PointType Type;
            public SegmentPoint(int coordinates, Segment.Type segmentType, PointType type)
            {
                Coordinates = coordinates;
                SegmentType = segmentType;
                Type = type;
            }
            public enum PointType
            {
                Start,
                End
            }
        }
    }
}
