using System.Collections.Generic;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainer.TagCloudVisualization
{
    public interface ILayouter
    {
        IEnumerable<TagCloudItem> PlaceWords(IEnumerable<WordData> words);
    }
}