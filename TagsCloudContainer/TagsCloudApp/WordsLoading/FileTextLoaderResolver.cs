using System;
using System.Collections.Generic;

namespace TagsCloudApp.WordsLoading
{
    public class FileTextLoaderResolver : IFileTextLoaderResolver
    {
        private readonly Dictionary<FileType, IFileTextLoader> loadersResolver = new();

        public FileTextLoaderResolver(IEnumerable<IFileTextLoader> loaders)
        {
            foreach (var loader in loaders)
            {
                foreach (var fileType in loader.SupportedTypes)
                {
                    if (loadersResolver.TryGetValue(fileType, out var existingLoader))
                        throw new Exception(
                            $"Duplicated {nameof(IFileTextLoader)} for key {nameof(fileType)}. "
                            + $"{existingLoader.GetType()} and {loader.GetType()}");

                    loadersResolver[fileType] = loader;
                }
            }
        }

        public IFileTextLoader GetFileTextLoader(FileType type) => loadersResolver[type];
    }
}