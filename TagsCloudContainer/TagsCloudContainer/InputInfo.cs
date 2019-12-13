namespace TagsCloudContainer
{
    public class InputInfo
    {
        public readonly string FileName;
        public readonly int MaxWordsCnt;
        public readonly string ImageFormat;

        public InputInfo(string filename, int maxWordsCnt = int.MaxValue, string imageFormat = "png")
        {
            FileName = filename;
            MaxWordsCnt = maxWordsCnt;
            ImageFormat = imageFormat;
        }
    }
}