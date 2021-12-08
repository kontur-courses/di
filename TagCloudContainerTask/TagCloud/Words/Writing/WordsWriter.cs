using System;
using System.Collections.Generic;
using System.Text;
using TagCloud.Words.Writing.ToConsole;
using TagCloud.Words.Writing.ToFile;

namespace TagCloud.Words.Writing
{
    public class WordsWriter : IWordsWriter
    {
        private readonly IConsoleWriter consoleWriter;
        private readonly IFileWriter fileWriter;

        public WordsWriter(IFileWriter fileWriter, IConsoleWriter consoleWriter)
        {
            this.fileWriter = fileWriter;
            this.consoleWriter = consoleWriter;
        }

        public void TypeToConsole()
        {
            if (consoleWriter == null)
                throw CreateArgumentNullException(nameof(consoleWriter));

            consoleWriter.TypeToConsole();
        }

        public void WriteToConsole(IEnumerable<string> words)
        {
            ThrowIfWordsCollectionIsNull(words);

            if (consoleWriter == null)
                throw CreateArgumentNullException(nameof(consoleWriter));

            consoleWriter.WriteToConsole(words);
        }

        public void WriteToFile(string pathToFile, IEnumerable<string> words, Encoding encoding)
        {
            ThrowIfWordsCollectionIsNull(words);

            if (fileWriter == null)
                throw CreateArgumentNullException(nameof(fileWriter));

            fileWriter.WriteToFile(pathToFile, words, encoding ?? Encoding.UTF8);
        }

        private void ThrowIfWordsCollectionIsNull(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words), "Words collection can not be null");
        }

        private ArgumentNullException CreateArgumentNullException(
            string parameterName, string message = "Can not find suitable writer")
        {
            return new ArgumentNullException(parameterName, message);
        }
    }
}