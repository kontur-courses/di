using System;
using System.Collections.Generic;
using System.Text;
using TagCloud.Words.Reading.Console;
using TagCloud.Words.Reading.FromFile;

namespace TagCloud.Words.Reading
{
    public class WordsReader : IWordsReader
    {
        private IConsoleReader consoleReader;
        private IFileReader fileReader;

        public WordsReader(IFileReader fileReader, IConsoleReader consoleReader)
        {
            this.fileReader = fileReader;
            this.consoleReader = consoleReader;
        }

        public IEnumerable<string> ReadFromConsole()
        {
            return consoleReader?
                       .ReadFromConsole()
                   ?? throw CreateArgumentNullException(nameof(consoleReader));
        }

        public IEnumerable<string> ReadFromFile(string pathToFile, Encoding encoding)
        {
            return fileReader?
                       .ReadFromFile(pathToFile, encoding ?? Encoding.UTF8)
                   ?? throw CreateArgumentNullException(nameof(fileReader));
        }

        private ArgumentNullException CreateArgumentNullException(
            string parameterName, string message = "Can not find suitable reader")
        {
            return new ArgumentNullException(parameterName, message);
        }
    }
}