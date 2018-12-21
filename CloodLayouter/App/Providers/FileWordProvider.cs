using System.Collections.Generic;
using System.IO;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class FileWordProvider : IProvider<IEnumerable<string>>
    {
        private readonly string[] fileNames;

        public FileWordProvider(string[] fileNames)
        {
            this.fileNames = fileNames;
        }

        public IEnumerable<string> Get()
        {
            return Read();
        }

        private IEnumerable<string> Read()
        {
            foreach (var fileName in this.fileNames)
            {
                var stream = new StreamReader(fileName);
                var line = stream.ReadLine();
                while (line != null)
                {
                    yield return line.ToLower();
                    line = stream.ReadLine();
                }
            }
        }
    }
}