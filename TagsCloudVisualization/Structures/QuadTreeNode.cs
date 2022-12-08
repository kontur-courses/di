using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Structures
{
    public class QuadTreeNode
    {
        private readonly Stack<Rectangle> contents = new();
        private QuadTreeNode[] nodes = Array.Empty<QuadTreeNode>();

        public QuadTreeNode(Rectangle bounds)
        {
            Bounds = bounds;
        }

        public bool IsEmpty => Bounds.IsEmpty || nodes.Length == 0;

        public Rectangle Bounds { get; }

        public int Count
        {
            get
            {
                return nodes.Sum(t => t.Count) + contents.Count;
            }
        }

        public bool IntersectsWith(Rectangle queryArea)
        {
            if (contents.Any(queryArea.IntersectsWith))
                return true;

            foreach (var node in nodes)
            {
                if (node.IsEmpty)
                    continue;

                if (node.Bounds.Contains(queryArea) && node.IntersectsWith(queryArea))
                    return true;

                if (queryArea.Contains(node.Bounds))
                    return true;

                if (node.Bounds.IntersectsWith(queryArea) && node.IntersectsWith(queryArea))
                    return true;
            }

            return false;
        }

        public void Insert(Rectangle item)
        {
            if (!Bounds.Contains(item))
                throw new ArgumentOutOfRangeException(nameof(item),
                    "Feature is out of the bounds of this quadtree node.");

            if (nodes.Length == 0)
                CreateSubNodes();

            foreach (var node in nodes)
                if (node.Bounds.Contains(item))
                {
                    node.Insert(item);
                    return;
                }

            contents.Push(item);
        }

        private void CreateSubNodes()
        {
            if (Bounds.Height * Bounds.Width <= 10)
                return;

            var halfWidth = Bounds.Width / 2;
            var halfHeight = Bounds.Height / 2;

            nodes = new[]
            {
                new QuadTreeNode(new Rectangle(Bounds.Location, new Size(halfWidth, halfHeight))),
                new QuadTreeNode(new Rectangle(new Point(Bounds.Left, Bounds.Top + halfHeight),
                    new Size(halfWidth, halfHeight))),
                new QuadTreeNode(new Rectangle(new Point(Bounds.Left + halfWidth, Bounds.Top),
                    new Size(halfWidth, halfHeight))),
                new QuadTreeNode(new Rectangle(new Point(Bounds.Left + halfWidth, Bounds.Top + halfHeight),
                    new Size(halfWidth, halfHeight)))
            };
        }
    }
}