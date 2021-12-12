using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.FileReader
{
    public class FileReadersResolver : IResolver<string, IFileReader>
    {
        private readonly Dictionary<string, IFileReader> fileReadersResolver;


        public FileReadersResolver(IFileReader[] readers)
        {
            fileReadersResolver = readers.ToDictionary(x => x.Extension);
        }

        public IFileReader Get(string path)
        {
            var ext = Path.GetExtension(path);
            if (fileReadersResolver.ContainsKey(ext))
                return fileReadersResolver[ext];
            throw new ArgumentException($"Формат {path} не поддерживается");
        }
    }
}