namespace TagCloud2.Image
{
    public class FileGenerator : IFileGenerator
    {
        public void GenerateFile(string name, IImageFormatter formatter, System.Drawing.Image image)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            image.Save(name, formatter.Codec, formatter.Parameters);
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}
