using System;
using System.IO;
using System.Linq;
using TagsCloudVisualisation.InputStream.FileInputStream.Exceptions;

namespace TagsCloudVisualisation.InputStream.FileInputStream
{
    public class TxtEncoder : IFileEncoder
    {
        private const string FileType = "txt";

        public string GetText(string fileName)
        {
            if (!IsCompatibleFileType(FileType))
                throw new IncorrectFileTypeException(FileType, fileName
                    .Split('.')
                    .LastOrDefault() ?? string.Empty);
            try
            {
                return File.ReadAllText(fileName);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Can not read words from file", e);
            }
            
        }

        public bool IsCompatibleFileType(string fileName)
        {
            return fileName.EndsWith(FileType);
        }

        public string GetExpectedFileType()
        {
            return FileType;
        }
    }
}