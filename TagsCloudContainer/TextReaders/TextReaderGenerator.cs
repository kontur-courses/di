using System;
using System.IO;

namespace TagsCloudContainer.TextReaders
{
    public class TextReaderGenerator
    {
        public ITextReader GetReader(string pathToFile)
        {
            var extension = Path.GetExtension(pathToFile);
            return extension switch
            {
                ".txt" => new TxtReader(),
                ".docx" => new WordReader(),
                _ => throw new ArgumentException($"This file format is not supported: {extension}")
            };
        }
    }
}