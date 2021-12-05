using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Layouter
{
    public interface ICircularCloudLayouter
    {
        RectangleWithWord PutNextElement(Size rectangleSize, Word element);
        void FillInElements(Size elementSize, List<Word> wordList);

        List<RectangleWithWord> GetElementsList();
    }
}
