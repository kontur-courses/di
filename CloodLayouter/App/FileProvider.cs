using System.IO;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class FileProvider : IFileProvider
    {
        public StreamReader File { get; set; } = new StreamReader("file.txt");
    }
}