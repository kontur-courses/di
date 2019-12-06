using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainer.TagCloudVisualization
{
    public interface ILayouter
    {
        int LayoutHeight { get; }
        int LayoutWidth { get; }
        TagCloudItem PlaceNextWord(WordData word, Size size);
    }
}