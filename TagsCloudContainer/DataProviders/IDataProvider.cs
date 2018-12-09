using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.DataProviders
{
    public interface IDataProvider
    {
        IReadOnlyDictionary<string, (Rectangle, int)> GetData();
    }
}