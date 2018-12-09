using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudVisualization.WordsProcessing
{
    public class TxtReader : IWordsProvider
    {
        private readonly string filename;
        
        public TxtReader(string filename)
        {
            this.filename = filename;
        }

        public IEnumerable<string> Provide()
        {
            using (var streamReader = new StreamReader(filename, Encoding.UTF8))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return streamReader.ReadLine()?.Trim(Environment.NewLine.ToCharArray());
                }
            }
        }
    }
}