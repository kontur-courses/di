using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public interface ITagsCloud
    {
        Point Center { get; }
        ReadOnlyCollection<Rectangle> AddedRectangles { get; }
        ReadOnlyCollection<TagsCloudWord> AddedWords { get; }

        void AddRectangle(Rectangle rectangle);
        void AddWord(TagsCloudWord word);
    }
}