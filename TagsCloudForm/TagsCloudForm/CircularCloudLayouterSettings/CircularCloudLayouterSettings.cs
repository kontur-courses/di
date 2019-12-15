namespace TagsCloudForm.CircularCloudLayouterSettings
{
    public class CircularCloudLayouterSettings:ICircularCloudLayouterSettings
    {
        public int CenterX { get; set; } = 200;

        public int CenterY { get; set; } = 200;

        public int MinSize { get; set; } = 10;

        public int MaxSize { get; set; } = 30;

        public int IterationsCount { get; set; } = 5;

        public int XCompression { get; set; } = 1;

        public int YCompression { get; set; } = 1;
    }
}
