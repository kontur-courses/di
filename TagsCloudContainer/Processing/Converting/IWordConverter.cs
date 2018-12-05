using System.Collections.Generic;

namespace TagsCloudContainer.Processing.Converting
{
    public interface IWordConverter
    {
        IEnumerable<string> Convert(IEnumerable<string> words);
    }
}