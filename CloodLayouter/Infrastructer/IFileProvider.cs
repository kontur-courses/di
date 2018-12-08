using System.IO;

namespace CloodLayouter.Infrastructer
{
    public interface IFileProvider
    {
        StreamReader File { get; set; }
    }
}