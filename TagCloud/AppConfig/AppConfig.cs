using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.ImageProcessing;

namespace TagCloud.AppConfig
{
    internal class AppConfig : IAppConfig
    {
        public string inputTextFilePath { get; set; }
        public string outputImageFileFullPath { get; set; }
        public IImageSettings imageSettings { get; set; }

        public AppConfig(string inputTextFileFullPath, string outputImageFileFullPath, IImageSettings imageSettings)
        {
            this.inputTextFilePath = inputTextFileFullPath;
            this.outputImageFileFullPath = outputImageFileFullPath;
            this.imageSettings = imageSettings;
        }
    }
}
 