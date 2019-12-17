using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SyntaxTextParser.Architecture;

namespace SyntaxTextParser
{
    public sealed class TextParser
    {
        private readonly ElementParserWithRules elementParserWithRules;
        private readonly IFileReader[] fileReaders;

        public TextParser(ElementParserWithRules elementParserWithRules, IFileReader[] fileReaders)
        {
            this.elementParserWithRules = elementParserWithRules;
            this.fileReaders = fileReaders;
        }

        /// <exception cref="ArgumentException">Thrown parser can't read that type file</exception>
        /// <exception cref="FileNotFoundException">Thrown parser can't found file</exception>
        public List<TextElement> ParseElementsFromFile(string path, string fileName, string type)
        {
            var fullPath = Path.Combine(path, fileName + '.' + type);
            return ParseElementsFromFile(fullPath);
        }

        public List<TextElement> ParseElementsFromFile(string fullPath)
        {
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"File {fullPath} isn't valid");

            var type = Path.GetExtension(fullPath);
            if (string.IsNullOrEmpty(type)) 
                throw new ArgumentException($"Incorrect extension in file path {fullPath}");
            type = type.Substring(1);

            var reader = fileReaders.FirstOrDefault(x => x.CanReadThatType(type));
            if (reader == null)
                throw new ArgumentException($"Parser can't read [{type}] file type");

            return reader.TryReadText(fullPath, out var text)
                ? elementParserWithRules.ParseElementsFromText(text)
                : new List<TextElement>();
        }
    }
}