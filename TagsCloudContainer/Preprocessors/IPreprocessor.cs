using System.Collections.Generic;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public interface IPreprocessor
    {
        IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags);
    }
}