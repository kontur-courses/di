namespace TagsCloudVisualization.ImageSaver
{
    public interface IImageSaverFactory
    {
        IImageSaver GetSaver(ImageExt ext);
    }
}