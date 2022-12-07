using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public class FileSettings
    {
        public static string PathToProj = string.Join("\\", Environment.CurrentDirectory.Split('\\').SkipLast(3))
            + @"\Sources";
        public string ResultImagePath { get; set; } = PathToProj;
        public string SourceFilePath { get; set; } = PathToProj + @"\source.txt";
        public string BoringWordsFilePath { get; set; } = PathToProj + @"\boring.txt";
    }
}
