using System;
using System.IO;
using System.Text;

namespace TagsCloudVisualizationDI.FileReader
{
    public class DefaultTextFileReader : ITextFileReader
    {
        public DefaultTextFileReader(string filePath, string saveAnalizationPath, string mystemPath, string arguments, Encoding enconding)
        {
            FilePath = filePath;
            SaveAnalizationPath = saveAnalizationPath;
            MystemPath = mystemPath;
            Arguments = arguments;
            ReadingEncoding = enconding;

        }


        public string FilePath { get; }


        public string SaveAnalizationPath { get; }
        public string MystemPath { get; }
        public string Arguments { get; }
        public Encoding ReadingEncoding { get; }

        //!!!!!

        //public string SaveAnalizationPath => "C:/GitHub/di/TagsCloudVisualizationDI/result.TXT";

        //public string MystemPath => "C:/GitHub/di/TagsCloudVisualizationDI/mystem.exe";

        //public string Arguments => "-lndw -ig";
        //public Encoding ReadingEncoding => Encoding.UTF8;
        //!!!!!!!



        public string[] ReadText(string path, Encoding encoding)
        {
            if (File.Exists(path))
            { 
                return File.ReadAllLines(path, encoding);
            }
            
            throw new Exception($"Giving path {path} is not valid");
        }
    }
}
