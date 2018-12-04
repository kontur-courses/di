using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
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