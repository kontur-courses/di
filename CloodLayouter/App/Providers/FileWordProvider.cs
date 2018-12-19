using System.Collections.Generic;
using System.IO;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class FileWordProvider : IProvider<IEnumerable<string>>
    {
        private readonly IProvider<StreamReader>[] fileProviders;

        public FileWordProvider(IProvider<StreamReader>[] fileProviders)
        {
            this.fileProviders = fileProviders;
        }

        public List<string> Read()
        {
            var wordList = new List<string>();
            foreach (var file in fileProviders)
            {
                var line = file.Get().ReadLine();
                while (line != null)
                {
                    wordList.Add(line.ToLower());
                    line = file.Get().ReadLine();
                }
            }
            return wordList;
        }

        public IEnumerable<string> Get()
        {
            return Read();
        }
    }
}