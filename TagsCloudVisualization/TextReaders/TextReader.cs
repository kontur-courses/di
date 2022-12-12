using System;
using System.IO;

namespace TagsCloudVisualization.TextReaders
{
    internal class TextReader : ITextReader
    {
        public string Path { get; }

        public TextReader(string path)
        {
            Path = path;
        }

        public string[] Read()
        {
            if (File.Exists(Path))
            {
                var text = File.ReadAllText(Path);
                var separators = new[] 
                { 
                    ' ', ',', '.', ';', ':', '<', '>', '[', ']',
                    '{', '}', '"', '(', ')','\'', '~',
                    '?', '!','$', '|', '_', '&', '#', '@', '^', '%',
                    '+', '=', '-', '/', '*', '\\', '|',
                    '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
                    '\n', '\r', '\t' 
                };

                return text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }

            throw new FileNotFoundException("File at the specified path does not exist");
        }
    }
}
