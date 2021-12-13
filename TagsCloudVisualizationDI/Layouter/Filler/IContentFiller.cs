using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualizationDI.Layouter.Filler
{
    public interface IContentFiller
    {
        void FillInElements(Size elementSize, List<Word> wordList);

        List<RectangleWithWord> GetElementsList();
        Dictionary<string, RectangleWithWord> FormElements(Size elementSize, List<Word> startElements);
        List<RectangleWithWord> PositionElements(List<RectangleWithWord> sizedElements);
    }
}
