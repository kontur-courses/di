using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer
{
    public interface ITagsCloud
    {
        Point Center { get; }
        List<Rectangle> AddedRectangles { get; }
        List<TagsCloudWord> AddedWords { get; }

        void AddRectangle(Rectangle rectangle);
        void AddWord(TagsCloudWord word);
    }
}