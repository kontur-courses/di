namespace TagsCloudContainer.CloudLayouter
{
    public interface ICloudLayouterSettings
    {
        int ImageWidth { get; }
        int ImageHeight { get; }
        int CenterX { get; }
        int CenterY { get; }
        int RotationAngle { get; }
    }
}