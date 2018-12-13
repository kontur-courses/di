using System.Collections.Generic;

namespace TagsCloudContainer.DataReader
{
    public interface IDataReader
    {
        IEnumerable<string> Read(string filename);
    }
}