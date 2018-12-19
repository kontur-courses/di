using System.ComponentModel;
using System.IO;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class SaveDirectoryProvider : IProvider<string>
    {

        private string imageFilePAthToSave;
        public SaveDirectoryProvider(string filePath)
        {
            imageFilePAthToSave = filePath;
        }
        
        public string Get()
        {
            return imageFilePAthToSave;
        }
    }
}