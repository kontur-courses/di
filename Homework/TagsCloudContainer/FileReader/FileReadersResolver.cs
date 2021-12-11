using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.FileReader
{
    public class FileReadersResolver : IFileReadersResolver
    {
        private readonly Dictionary<string, IFileReader> fileReadersResolver;


        public FileReadersResolver(IFileReader[] readers)
        {
            fileReadersResolver = readers.ToDictionary(x => x.Extension);
        }

        public IFileReader Get(string extension)
        {
            if (fileReadersResolver.ContainsKey(extension))
                return fileReadersResolver[extension];
            throw new ArgumentException($"Формат {extension} не поддерживается");
        }
    }
}