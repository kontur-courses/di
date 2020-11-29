using System.Collections.Generic;    

namespace TagCloud.DataReaders
{
    public interface IDataReader
    {
        public List<string> ReadWords(string filePath);
    }
}
