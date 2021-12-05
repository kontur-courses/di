using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Creators;

namespace TagCloud.Layouters
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly Point center;
        private readonly List<Tag> placedTags;
        private readonly HashSet<Point> usedPoints;

        public CircularCloudLayouter(Point center)
        {
            placedTags = new List<Tag>();
            this.center = center; 
            usedPoints = new HashSet<Point>();
        }

        public IEnumerable<Tag> PutTags(IEnumerable<Tag> tags)
        {
            return tags.Select(PutNextTag);
        }

        private Tag PutNextTag(Tag tag)
        {
            if (tag.Size.Height <= 0 || tag.Size.Width <= 0)
                throw new ArgumentException("Size Should be positive but was: " +
                                            $"Height: {tag.Size.Height}" +
                                            $"Width: {tag.Size.Width}");

            if (TryPutTagInCenter(tag, out var placedTag))
                return placedTag;
            
            var tagToPlace = GetNearestTag(tag);
            return tagToPlace;
        }

        private Tag GetNearestTag(Tag tag)
        {
            var potentialPoints = placedTags
                .SelectMany(t => GetNextTagPotentialPoints(t, tag));
            
            var potentialRectangles = potentialPoints.Select(p => new Rectangle(p, tag.Size))
                .Where(rect =>
                    placedTags.All(x => !x.ContainingRectangle.IntersectsWith(rect)));
            
            var nearestTag = new Tag(tag, GetNearestRectangle(potentialRectangles));
            usedPoints.Add(nearestTag.ContainingRectangle.Location);
            placedTags.Add(nearestTag);
            return nearestTag;
        }

        private Rectangle GetNearestRectangle(IEnumerable<Rectangle> potentialRectangles)
        {
            return potentialRectangles.Aggregate((nearest, current) =>
                current.GetCenter().DistanceTo(center) <
                nearest.GetCenter().DistanceTo(center)
                    ? current
                    : nearest);
        }

        private IEnumerable<Point> GetNextTagPotentialPoints(Tag tag, Tag tagToPlace)
        {
            var rectangle = tag.ContainingRectangle;
            var points = new[]
            {
                new Point(rectangle.Left, rectangle.Top - tagToPlace.Size.Height),
                new Point(rectangle.Right, rectangle.Top),
                new Point(rectangle.Left, rectangle.Bottom),
                new Point(rectangle.Left - tagToPlace.Size.Width, rectangle.Top)
            };
            return points.Where(p => !usedPoints.Contains(p));
        }

        private bool TryPutTagInCenter(Tag tag, out Tag placedTag)
        {
            placedTag = null;
            var rectangleX = center.X - tag.Size.Width / 2;
            var rectangleY = center.Y - tag.Size.Height / 2;
            var upperLeftCorner = new Point(rectangleX, rectangleY);
            if (placedTags.Any(t => new Rectangle(upperLeftCorner, tag.Size).IntersectsWith(t.ContainingRectangle)))
                return false;
            var rectangle = new Rectangle(upperLeftCorner, tag.Size);
            placedTag = new Tag(tag, rectangle);
            placedTags.Add(placedTag);
            usedPoints.Add(upperLeftCorner);
            return true;
        }
    }
}
