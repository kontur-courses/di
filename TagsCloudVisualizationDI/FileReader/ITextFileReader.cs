using System.Diagnostics;
using System.Text;

namespace TagsCloudVisualizationDI.FileReader
{
    public interface ITextFileReader
    {
        public string Arguments { get; }
        public string FilePath { get;}
        public string SavePath { get;}
        public string MystemPath { get;}

        public Encoding ReadingEncoding { get; }

        public void InvokeProcess()
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = MystemPath,
                Arguments = Arguments + ' ' + FilePath + ' ' + SavePath,
            });
            process.WaitForExit();
        }
        string[] ReadText(string path, Encoding encoding);
    }
}
