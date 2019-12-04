using System.Collections.Generic;
using TagsCloudContainer.Core.Layouters;
using TagsCloudContainer.Data;

namespace TagsCloudContainer.Visualization.Layouts
{
    public interface IWordsLayout
    {
        IEnumerable<Tag> PlaceWords(CircularCloudLayouter layouter, IEnumerable<Word> words);
    }
}