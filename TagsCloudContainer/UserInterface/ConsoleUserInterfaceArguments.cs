using System.Collections.Generic;

namespace TagsCloudContainer.UserInterface
{
    public class ConsoleUserInterfaceArguments
    {
        public string InputFilePath { get; set; }

        public string OutputFilePath { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Font { get; set; }

        public List<string> Colors { get; set; }

        public string ImageFormat { get; set; }
    }
}