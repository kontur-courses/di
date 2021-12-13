using System.Collections.Generic;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public interface IPreprocessor
    {
        public static State State { get; set; } = State.Active;
        IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags);
    }
}