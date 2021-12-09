using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace TagsCloudVisualizationDI.FileReader
{
    public class DefaultTextFileReader : ITextFileReader
    {

        public DefaultTextFileReader(string filePath, string savePath, string mystemPath, string args,
            Encoding textEncoding)
        {
            FilePath = filePath;
            SavePath = savePath;
            MystemPath = mystemPath;
            Arguments = args;
            ReadingEncoding = textEncoding;
        }

        //public string FilePath => "C:/GitHub/di/TagsCloudVisualizationDI/War_and_piece.Docx";
        public string FilePath { get; }
        public string SavePath { get; }
        public string MystemPath { get; }
        public string Arguments { get; }
        public Encoding ReadingEncoding { get; }


        //public string SavePath => "C:/GitHub/di/TagsCloudVisualizationDI/result.TXT";

        //public string MystemPath => "C:\\GitHub\\di\\TagsCloudVisualizationDI\\mystem.exe";

        //public string Arguments => "-lndw -ig";

        //public Encoding ReadingEncoding => Encoding.UTF8;

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
