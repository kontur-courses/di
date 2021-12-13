using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualizationDI.Layouter.Filler
{
    public interface IContentFiller
    {
        Dictionary<string, RectangleWithWord> FormElements(Size elementSize, List<Word> startElements);
        List<RectangleWithWord> MakePositionElements(List<RectangleWithWord> sizedElements);
    }
}
