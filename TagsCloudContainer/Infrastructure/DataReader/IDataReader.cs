using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.DataReader
{
    internal interface IDataReader
    {
        public IEnumerable<string> ReadLines();
    }
}