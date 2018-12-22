using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Infrastructure.FileManaging
{
    public class TemporaryFileManager : IFileManager
    {
        private readonly List<string> fileNames = new List<string>();

        public string MakeFile()
        {
            var fileName = Path.GetTempFileName();
            fileNames.Add(fileName);
            
            return fileName;
        }
        
        public void WriteTextInFile(string fileName, string text)
        {
            using (var file = new StreamWriter(fileName))
            {
                file.Write(text);
                file.Close();
            }
        }
        
        public string ReadTextFromFile(string fileName)
        {
            using (var file = new StreamReader(fileName))
            {
                var result = file.ReadToEnd();
                return result;
            }
        }
        
        public void DeleteAllFiles()
        {
            foreach (var fileName in fileNames)
                File.Delete(fileName);
        }
    }
}