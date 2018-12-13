using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public interface ITagsCloud
    {
        ReadOnlyCollection<Rectangle> AddedRectangles { get; }
        ReadOnlyCollection<TagsCloudWord> AddedWords { get; }

        void AddWord(TagsCloudWord word);
    }
}