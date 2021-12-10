using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualizationDI.Layouter.Filler
{
    public interface IContentFiller
    {
        void FillInElements(Size elementSize, List<Word> wordList);

        List<RectangleWithWord> GetElementsList();
    }
}
