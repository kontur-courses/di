using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TagsCloudVisualizationDI.FileReader
{
    public interface ITextFileReader
    {
        //public string FilePath { get; }
        //public string MystemArgs { get; }
        public string ReadingTextPath { get;}
        //public string MystemPath { get;}

        public Encoding ReadingEncoding { get; }

        /*
        public void InvokeMystemAnalization()
        {
            if (!File.Exists(ReadingTextPath))
                throw new Exception($"Giving path to file: {ReadingTextPath} is not valid, EXC");
            if (!File.Exists(MystemPath))
                throw new Exception($"Giving path to mystemFile: {MystemPath} is not valid, EXC");

            var process = Process.Start(new ProcessStartInfo
            {
                FileName = MystemPath,
                MystemArgs = MystemArgs + ' ' + FilePath + ' ' + ReadingTextPath,
            });
            process.WaitForExit();
        }
        */
        string[] ReadText(string path, Encoding encoding);
    }
}
