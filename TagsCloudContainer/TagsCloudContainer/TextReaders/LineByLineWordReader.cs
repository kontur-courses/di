using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class LineByLineWordReader : IWordReader
    {
        public LineByLineWordReader()
        {

        }

        public IEnumerable<string> Read(string pathToFile)
        {
            var streamReader = new StreamReader(pathToFile);
            var line = streamReader.ReadLine();

            while (line != null)
            {
                yield return line;

                line = streamReader.ReadLine();
            }

            streamReader.Close();
        }
    }
}
