using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.FileReader
{
    public class FileReadersesResolver : IFileReadersResolver
    {
        private readonly Dictionary<TextFileFormat, IFileReader> fileReadersResolver;


        public FileReadersesResolver(IFileReader[] readers)
        {
            fileReadersResolver = readers.ToDictionary(x => x.Format);
        }

        public IFileReader Get(TextFileFormat format)
        {
            if (fileReadersResolver.ContainsKey(format))
                return fileReadersResolver[format];
            throw new ArgumentException("Формат не поддерживается");
        }

    }
}