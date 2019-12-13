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
                    return new TxtFileReader(new WordsParser());
                case "doc":
                case "docx":
                    return new DocFileReader(new WordsParser());
                case "xml":
                    return new XmlFileReader();
                default:
                    throw new ArgumentException("not exist reader for this file");
            }
        }
    }
}