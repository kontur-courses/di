using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TagsCloudVisualization.WordReaders.FormatDecoders;

namespace TagsCloudVisualization.WordReaders
{
    public class FileTextByWordReader : IWordReader
    {
        private readonly IFormatDecoder formatDecoder;
        private string text;
        private int wordPointer;
        
        public FileTextByWordReader(string filename, IEnumerable<IFormatDecoder> supportedFormats)
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
            text = formatDecoder.Decode(allText);
        }

        public string Read()
        {
            if (!HasWord()) throw new InvalidOperationException("has no word anymore");
            var word = new StringBuilder();
            var s = ' ';
            do
            {
                s = text[wordPointer++];
                if (char.IsLetter(s)) word.Append(s);
            } while ((word.Length == 0 || char.IsLetter(s)) && HasWord());
            return word.ToString();
        }

        public bool HasWord()
        {
            return wordPointer < text.Length;
        }
    }
}