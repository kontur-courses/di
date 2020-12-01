namespace TagsCloudContainer
{
    public interface IOptions
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public string FontFamily { get; set; }

        public string FilePath { get; set; }

        public string OutputDirectory { get; set; }

        public string OutputFileName { get; set; }

        public string OutputFileExtension { get; set; }

        public string FontColor { get; set; }
    }
}