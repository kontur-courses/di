using System.Collections.Generic;
using TagCloudContainerTests;

namespace TagsCloudContainer
{
    public interface IPreprocessor
    {
        IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags);
    }
}