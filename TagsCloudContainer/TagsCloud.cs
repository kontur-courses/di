using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public class TagsCloud : ITagsCloud
    {
        public Point Center { get; }

        public ReadOnlyCollection<Rectangle> AddedRectangles => new ReadOnlyCollection<Rectangle>(addedRectangles);
        public ReadOnlyCollection<TagsCloudWord> AddedWords => new ReadOnlyCollection<TagsCloudWord>(addedWords);
        private List<Rectangle> addedRectangles { get; } = new List<Rectangle>();
        private List<TagsCloudWord> addedWords { get; } = new List<TagsCloudWord>();


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
            addedRectangles = rectangles;
        }

        public TagsCloud(Point center, List<TagsCloudWord> words)
        {
            var x = center.X;
            var y = center.Y;
            if (x < 0 || y < 0)
                throw new ArgumentException("Center coordinates should not be negative");
            addedRectangles = words.Select(w => w.Rectangle).ToList();
            addedWords = words;
        }

        public void AddRectangle(Rectangle rectangle)
        {
            addedRectangles.Add(rectangle);
        }

        public void AddWord(TagsCloudWord word)
        {
            addedWords.Add(word);
        }
    }
}