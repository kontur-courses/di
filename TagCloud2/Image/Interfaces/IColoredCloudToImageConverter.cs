namespace TagCloud2.Image
{
    public interface IColoredCloudToImageConverter
    {
        System.Drawing.Image GetImage(IColoredCloud cloud, int xSize, int ySize);
    }
}
