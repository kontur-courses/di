using System.IO;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class SaveDirectoryProvider : ISaveDirectoryProvider
    {
        public string DirectoryToSave { get; set; } =
            Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "saved.png";
    }
}