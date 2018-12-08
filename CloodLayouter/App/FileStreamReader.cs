using System.Collections.Generic;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class FileStreamReader : IStreamReader, IWordProvider
    {
        private readonly IFileProvider fileProvider;

        public FileStreamReader(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public List<string> Read()
        {
            var wordList = new List<string>();
            var line = fileProvider.File.ReadLine();
            while (line != null)
            {
                wordList.Add(line.ToLower());
                line = fileProvider.File.ReadLine();
            }

            return wordList;
        }

        List<string> IWordProvider.GetWords()
        {
            return Read();
        }
    }
}