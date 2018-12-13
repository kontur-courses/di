namespace TagsCloudContainer.ImageWriter
{
    public interface IImageWriter
    {
        void Write(byte[] image, string imageName, string imageFormat, string pathToSave);
    }
}