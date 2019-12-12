using System.Collections.Generic;
using System.IO;
using Xceed.Words.NET;

namespace TagsCloudContainer.Core.Readers
{
    class DocReader : IReader
    {
        public IEnumerable<string> ReadWords(string path) => DocX.Load(path).Text.Split();

        public bool CanRead(string path)
        {
            var extension = Path.GetExtension(path);
            return extension == ".doc" || extension == ".docx";
        }
    }
}