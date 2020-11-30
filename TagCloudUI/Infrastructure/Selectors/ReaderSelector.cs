using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.FileReaders;

namespace TagCloudUI.Infrastructure.Selectors
{
    public class ReaderSelector : IReaderSelector
    {
        private readonly Dictionary<string, IFileReader> extensionToReader;

        public ReaderSelector(IEnumerable<IFileReader> readers)
        {
            extensionToReader = readers.ToDictionary(r => r.Extension,
                StringComparer.OrdinalIgnoreCase);
        }

        public bool TryGetReader(string extension, out IFileReader reader)
        {
            return extensionToReader.TryGetValue(extension, out reader);
        }
    }
}