using System.Collections.Generic;
using TextPreprocessor.Core;

namespace TagCloudPainter
{
    public interface ITagCloudPainter
    {
        void Draw(IEnumerable<TagInfo> tagInfos);
    }
}