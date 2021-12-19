namespace TagCloud2
{
    public interface IFileGenerator
    {
        void GenerateFile(string name, IImageFormatter formatter, System.Drawing.Image image);
    }
}
