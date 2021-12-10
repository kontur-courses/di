using System.Collections.Generic;

namespace TagsCloudApp.WordsLoading
{
    public class FileTextLoaderResolver : IFileTextLoaderResolver
    {
        private readonly Dictionary<FileType, IFileTextLoader> loadersResolver = new();

        public FileTextLoaderResolver(IEnumerable<IFileTextLoader> loaders)
        {
            foreach (var loader in loaders)
            foreach (var fileType in loader.SupportedTypes)
                loadersResolver.Add(fileType, loader);
        }

        public IFileTextLoader GetFileTextLoader(FileType type) => loadersResolver[type];
    }
}