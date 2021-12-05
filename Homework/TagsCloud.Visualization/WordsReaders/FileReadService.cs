using System;
using System.IO;
using System.Linq;
using TagsCloud.Visualization.WordsReaders.FileReaders;

namespace TagsCloud.Visualization.WordsReaders
{
    public class FileReadService : IFileReadService
    {
        private readonly IFileReader[] fileReaders;

        public FileReadService(IFileReader[] fileReaders)
        {
            this.fileReaders = fileReaders;
        }
        
        public string Read(string filename)
        {
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));
            
            var fileExtension = Path.GetExtension(filename);
            var reader = fileReaders.FirstOrDefault(x => x.Extension == fileExtension);
            
            if (reader == null)
                throw new ArgumentException($"Unsupported file extension: {fileExtension}");

            if (!File.Exists(filename))
                throw new ArgumentException($"File {filename} doesn't exists");

            return reader.Read(filename);
        }
    }
}