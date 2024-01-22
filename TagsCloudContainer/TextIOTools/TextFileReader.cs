using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.TextTools
{
    public class TextFileReader : IFileReader
    {
        public string ReadTextFromFile(string filePath)
        {
            //try catch?
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
