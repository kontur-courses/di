using System.Collections.Generic;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    internal class OptionsStub: IOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string FontFamily { get; set; }
        public string FilePath { get; set; }
        public string OutputDirectory { get; set; }
        public string OutputFileName { get; set; }
        public string OutputFileExtension { get; set; }
        public string FontColor { get; set; }
        public string MystemLocation { get; set; }
        public IEnumerable<string> BoringWords { get; set; }

        public OptionsStub(int width, int height)
        {
            Width = width;
            Height = height;
            BoringWords = new List<string>();
        }
    }
}