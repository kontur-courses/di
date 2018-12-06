using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface IPreprocessor
    {
        IEnumerable<string> Process();
    }
}