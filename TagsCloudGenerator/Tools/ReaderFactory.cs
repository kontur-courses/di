using System;
using TagsCloudGenerator.FileReaders;

namespace TagsCloudGenerator.Tools
{
    public static class ReaderFactory
    {
        public static IFileReader GetReaderFor(string fileExtension)
        {
            switch (fileExtension)
            {
                case "txt":
                    return new TxtFileReader();
                case "doc":
                case "docx":
                    return new DocFileReader();
                case "xml":
                    return new XmlFileReader();
                default:
                    throw new ArgumentException("not exist reader for this file");
            }
        }
    }
}