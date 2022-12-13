using TagsCloudContainer.Core.Options.Interfaces;

namespace TagsCloudContainer.Core.Tests
{
    internal class TestOptions : IFilterOptions, IImageOptions
    {
        public string? MyStemLocation { get; set; }
        public IEnumerable<string> BoringWords { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ImageOutputDirectory { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }

        public TestOptions(int width, int height)
        {
            Width = width;
            Height = height;
            BoringWords = new List<string>();
        }
    }
}
