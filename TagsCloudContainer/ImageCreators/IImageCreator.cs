using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.WordsHandlers;

namespace TagsCloudContainer.ImageCreators
{
    public interface IImageCreator
    {
        Image GetImage(IEnumerable<WordInfo> wordInfos);
    }
}