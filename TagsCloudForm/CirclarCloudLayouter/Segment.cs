using System;
using System.Collections.Generic;
using System.Drawing;

namespace CircularCloudLayouter
{
    public class Segment
    {
        public Point Start { get; }
        public  Point End { get; }
        public  Type SegmentType { get; }
        public double Length  => Math.Sqrt(Math.Pow((Start.X - End.X), 2) + Math.Pow((Start.Y - End.Y), 2)); 
        public Segment(Point start, Point end, Type type)
        {
            if (Segment.Horizontal(type))
            {
                if (start.Y != end.Y)
                    throw new ArgumentException("Wrong coordinates");

                Start = start.X < end.X ? start : end;
                End = start.X < end.X ? end : start;
                SegmentType = type;
            }
            else
            {
                if (start.X != end.X)
                    throw new ArgumentException("Wrong coordinates");

                Start = start.Y < end.Y ? start : end;
                End = start.Y < end.Y ? end : start;
                SegmentType = type;
            }
        }
        public Segment(int startX, int startY, int endX, int endY, Type type) : this(new Point(startX, startY), new Point(endX, endY), type)
        {
        }

        public bool Parallel(Segment segment)
        {
            if (segment.SegmentType == Type.Top && SegmentType == Type.Top
                || segment.SegmentType == Type.Bottom && SegmentType == Type.Top
                || segment.SegmentType == Type.Top && SegmentType == Type.Bottom
                || segment.SegmentType == Type.Bottom && SegmentType == Type.Bottom)
                return true;

            if (segment.SegmentType == Type.Left && SegmentType == Type.Left
                || segment.SegmentType == Type.Right && SegmentType == Type.Left
                || segment.SegmentType == Type.Left && SegmentType == Type.Right
                || segment.SegmentType == Type.Right && SegmentType == Type.Right)
                return true;

            return false;
        }

        public bool Opposite(Segment segment)
        {
            if (segment.SegmentType == Type.Bottom && SegmentType == Type.Top
                || segment.SegmentType == Type.Top && SegmentType == Type.Bottom)
                return true;

            if (segment.SegmentType == Type.Right && SegmentType == Type.Left
                || segment.SegmentType == Type.Left && SegmentType == Type.Right)
                return true;

            return false;
        }

        public bool Horizontal()
        {
            if (SegmentType == Type.Bottom || SegmentType == Type.Top)
                return true;

            return false;
        }

        public static bool Horizontal(Type type)
        {
            return type == Type.Bottom || type == Type.Top;
        }


        public bool Intersects(Segment segment)
        {
            if (Horizontal() && segment.Horizontal())
            {
                if ((Start.X >= segment.Start.X && Start.X < segment.End.X)
                            || (End.X > segment.Start.X && End.X <= segment.End.X)
                            || (Start.X <= segment.Start.X && End.X >= segment.End.X)
                            || (Start.X >= segment.Start.X && End.X <= segment.End.X))
                    return true;

                return false;
            }
            if (!Horizontal() && !segment.Horizontal())
            {
                if ((Start.Y >= segment.Start.Y && Start.Y < segment.End.Y)
                            || (End.Y > segment.Start.Y && End.Y <= segment.End.Y)
                            || (Start.Y <= segment.Start.Y && End.Y >= segment.End.Y)
                            || (Start.Y >= segment.Start.Y && End.Y <= segment.End.Y))
                    return true;

                return false;
            }
            return false;
        }


        public static Type OppositeType(Type type)
        {
            switch (type)
            {
                case Type.Top:
                    return Type.Bottom;
                case Type.Bottom:
                    return Type.Top;
                case Type.Left:
                    return Type.Right;
                case Type.Right:
                    return Type.Left;
                default:
                    return Type.Top;
            }
        }


        public static HashSet<Segment> GetSegmentsFromRectangle(Rectangle rect)
        {
            var outSegments = new HashSet<Segment>();
            outSegments.Add(new Segment(
                    rect.X
                    , rect.Y
                    , rect.X + rect.Width
                    , rect.Y
                    , Type.Top));
            outSegments.Add(new Segment(
                rect.X
                , rect.Y + rect.Height
                , rect.X + rect.Width
                , rect.Y + rect.Height
                , Type.Bottom));
            outSegments.Add(new Segment(
                rect.X
                , rect.Y
                , rect.X
                , rect.Y + rect.Height
                , Type.Left));
            outSegments.Add(new Segment(
                rect.X + rect.Width
                , rect.Y
                , rect.X + rect.Width
                , rect.Y + rect.Height
                , Type.Right));
            return outSegments;
        }

        public enum Type
        {
            Top,
            Bottom,
            Left,
            Right
        }

    }
}
