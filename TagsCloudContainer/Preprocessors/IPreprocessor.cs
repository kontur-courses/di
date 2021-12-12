using System.Collections.Generic;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    [State(State.Active)]
    public interface IPreprocessor
    {
        IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags);
    }
}