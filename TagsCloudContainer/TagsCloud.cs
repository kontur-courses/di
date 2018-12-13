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
        public ReadOnlyCollection<Rectangle> AddedRectangles => new ReadOnlyCollection<Rectangle>(addedRectangles);
        public ReadOnlyCollection<TagsCloudWord> AddedWords => new ReadOnlyCollection<TagsCloudWord>(addedWords);
        private List<Rectangle> addedRectangles { get; } = new List<Rectangle>();
        private List<TagsCloudWord> addedWords { get; } = new List<TagsCloudWord>();


        public TagsCloud()
        {
        }

        public TagsCloud(List<TagsCloudWord> words)
        {
            addedRectangles = words.Select(w => w.Rectangle).ToList();
            addedWords = words;
        }

        

        public void AddWord(TagsCloudWord word)
        {
            addedWords.Add(word);
            addedRectangles.Add(word.Rectangle);
        }
    }
}