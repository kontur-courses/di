using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class FileStreamReader : IStreamReader
    {       
        public IEnumerable<string> Read(string filename)
        {
            using (var fileStream = new StreamReader(filename))
            {
                var line = fileStream.ReadLine();
                while (line != null)
                {
                    yield return line;
                    line = fileStream.ReadLine();
                }
            }
        }
    }
}