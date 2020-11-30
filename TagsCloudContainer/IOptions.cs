namespace TagsCloudContainer
{
    public interface IOptions
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public string Font { get; set; }

        public string FilePath { get; set; }

        public string Output { get; set; }
    }
}