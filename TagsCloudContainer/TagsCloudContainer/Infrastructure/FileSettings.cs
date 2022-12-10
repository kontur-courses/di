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
        public string ResultImagePath { get; set; } = PathToProjResources;
        public string SourceFilePath { get; set; } = PathToProjResources + @"\source.txt";
        public string CustomBoringWordsFilePath { get; set; } = PathToProjResources + @"\boring.txt";

        public void ThrowExcIfFileNotFound()
        {
            if (!File.Exists(SourceFilePath))
                throw new FileNotFoundException($"Ресурсного файла со словами не существует {SourceFilePath}");
            if (!File.Exists(SourceFilePath))
                throw new FileNotFoundException($"Ресурсного файла со скучными словами не существует {SourceFilePath}");
        }
    }
}
