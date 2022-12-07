using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public class FileSettings
    {
        public string ResultImagePath { get; set; } = Environment.CurrentDirectory + @"\image.png";
        public string SourceFilePath { get; set; } = Environment.CurrentDirectory + @"\source.txt";
        public string BoringWordsFilePath { get; set; } = Environment.CurrentDirectory + @"\boring.txt";
    }
}
