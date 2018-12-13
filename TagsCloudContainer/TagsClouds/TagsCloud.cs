using System.Collections.Generic;
using System.Collections.ObjectModel;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer.TagsClouds
{
    public class TagsCloud : ITagsCloud
    {
        public ReadOnlyCollection<TagsCloudWord> AddedWords => new ReadOnlyCollection<TagsCloudWord>(addedWords);
        private readonly List<TagsCloudWord> addedWords = new List<TagsCloudWord>();


        public TagsCloud()
        {
        }

        public TagsCloud(List<TagsCloudWord> words)
        {
            addedWords = words;
        }


        public void AddWord(TagsCloudWord word)
        {
            addedWords.Add(word);
        }
    }
}