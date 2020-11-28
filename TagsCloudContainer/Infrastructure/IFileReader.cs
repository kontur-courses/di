using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure
{
    internal interface IFileReader
    {
        public IEnumerable<string> ReadLines(string filename);
    }
}