using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.FileReader
{
    internal interface IFileReader
    {
        public IEnumerable<string> ReadLines(string filename);
    }
}