using System.Collections.ObjectModel;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer.TagsClouds
{
    public interface ITagsCloud
    {
        ReadOnlyCollection<TagsCloudWord> AddedWords { get; }

        void AddWord(TagsCloudWord word);
    }
}