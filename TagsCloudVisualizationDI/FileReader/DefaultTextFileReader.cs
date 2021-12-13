using System;
using System.IO;
using System.Text;

namespace TagsCloudVisualizationDI.FileReader
{
    public class DefaultTextFileReader : ITextFileReader
    {
        public DefaultTextFileReader(string readingTextPath, Encoding enconding)
        {
            //FilePath = filePath;
            ReadingTextPath = readingTextPath;
            //MystemPath = mystemPath;
            //MystemArgs = arguments;
            ReadingEncoding = enconding;
        }


        //public string FilePath { get; }


        public string ReadingTextPath { get; }
        //public string MystemPath { get; }
        //public string MystemArgs { get; }
        public Encoding ReadingEncoding { get; }


        public string[] ReadText(string path, Encoding encoding)
        {
            if (File.Exists(path))
            { 
                return File.ReadAllLines(path, encoding);
            }
            
            throw new FileNotFoundException($"Giving path {path} is not valid");
        }
    }
}
