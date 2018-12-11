using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public class TagsCloud : ITagsCloud
    {
        public Point Center { get; }
        public List<Rectangle> AddedRectangles { get; } = new List<Rectangle>();
        public List<TagsCloudWord> AddedWords { get; } = new List<TagsCloudWord>();


        public TagsCloud(Point center)
        {
            var x = center.X;
            var y = center.Y;
            if (x < 0 || y < 0)
                throw new ArgumentException("Center coordinates should not be negative");

            Center = center;
        }

        public TagsCloud(Point center, List<Rectangle> rectangles)
        {
            var x = center.X;
            var y = center.Y;
            if (x < 0 || y < 0)
                throw new ArgumentException("Center coordinates should not be negative");
            AddedRectangles = rectangles;
        }

        public TagsCloud(Point center, List<TagsCloudWord> words)
        {
            var x = center.X;
            var y = center.Y;
            if (x < 0 || y < 0)
                throw new ArgumentException("Center coordinates should not be negative");
            AddedRectangles = words.Select(w => w.Rectangle).ToList();
            AddedWords = words;
        }

        public void AddRectangle(Rectangle rectangle)
        {
            AddedRectangles.Add(rectangle);
        }

        public void AddWord(TagsCloudWord word)
        {
            AddedWords.Add(word);
        }
    }
}