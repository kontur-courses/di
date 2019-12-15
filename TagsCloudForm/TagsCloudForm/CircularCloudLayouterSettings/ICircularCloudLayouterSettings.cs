namespace TagsCloudForm.CircularCloudLayouterSettings
{
    public interface ICircularCloudLayouterSettings
    {
        int CenterX { get; set; }

        int CenterY { get; set; }

        int MinSize { get; set; }

        int MaxSize { get; set; }

        int IterationsCount { get; set; }

        int XCompression { get; set; }

        int YCompression { get; set; }
    }
}
