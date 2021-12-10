using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TagsCloudVisualizationDI.FileReader
{
    public interface ITextFileReader
    {
        public string Arguments { get; }
        public string FilePath { get;}
        public string SaveAnalizationPath { get;}
        public string MystemPath { get;}

        public Encoding ReadingEncoding { get; }

        public void InvokeProcess()
        {
            if (!File.Exists(SaveAnalizationPath))
                throw new Exception($"Giving path to file: {SaveAnalizationPath} is not valid, EXC");
            if (!File.Exists(MystemPath))
                throw new Exception($"Giving path to mystemFile: {MystemPath} is not valid, EXC");

            var process = Process.Start(new ProcessStartInfo
            {
                FileName = MystemPath,
                Arguments = Arguments + ' ' + FilePath + ' ' + SaveAnalizationPath,
            });
            process.WaitForExit();
        }
        string[] ReadText(string path, Encoding encoding);
    }
}
