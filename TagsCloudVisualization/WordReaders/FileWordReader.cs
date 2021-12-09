using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.WordReaders.FormatDecoders;

namespace TagsCloudVisualization.WordReaders
{
    public class FileWordReader : IWordReader
    {
        private readonly IFormatDecoder formatDecoder;
        private string[] text;
        private uint wordPointer;
        
        public FileWordReader(string filename, IEnumerable<IFormatDecoder> supportedFormats)
        {
            if (!File.Exists(filename)) throw new FileNotFoundException($"file {filename} does not exist");
            Console.WriteLine(filename);
            formatDecoder = supportedFormats.FirstOrDefault(x => x.FormatExtension == Path.GetExtension(filename)) 
                            ?? throw new ArgumentException($"no decoder for {Path.GetExtension(filename)} provided");
            Load(filename);
        }

        private void Load(string filename)
        {
            var allText = File.ReadAllText(filename);
            text = formatDecoder.Decode(allText).Split(new string[2]{"\n", "\r\n"}, StringSplitOptions.None);
        }

        public string Read()
        {
            if (!HasWord()) throw new InvalidOperationException("has no word anymore");
            var word = text[wordPointer++];
            if (!word.All(char.IsLetter)) throw new InvalidDataException($"word {word} contain invalid symbols");
            return word;
        }

        public bool HasWord()
        {
            return wordPointer < text.Length;
        }
    }
}