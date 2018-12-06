namespace CloudLayouter.Infrastructer.Common.Settings
{
    public class TagSettings
    {
        public int MinWidth { get; set; } = 20;
        public int MaxWidth { get; set; } = 50;

        public int MinHeight { get; set; } = 10;
        public int MaxHeight { get; set; } = 40;

        public int CountOfTags { get; set; } = 40;
    }
}