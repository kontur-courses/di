using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public class FileSettings
    {
        public static string PathToProjResources = string.Join("\\", Environment.CurrentDirectory.Split('\\').SkipLast(3))
            + @"\Sources";
        private string resultImagePath = PathToProjResources;
        private string sourceFilePath = PathToProjResources + @"\source.txt";
        private string customBoringWordsFilePath = PathToProjResources + @"\boring.txt";


        public string ResultImagePath
        {
            get => resultImagePath;
            set => resultImagePath =  File.Exists(value) ? value : resultImagePath;
        }
        public string SourceFilePath
        {
            get => sourceFilePath;
            set => sourceFilePath = File.Exists(value) ? value : sourceFilePath;
        }
        public string CustomBoringWordsFilePath
        {
            get => customBoringWordsFilePath;
            set => customBoringWordsFilePath = File.Exists(value) ? value : customBoringWordsFilePath;
        }
    }
}
