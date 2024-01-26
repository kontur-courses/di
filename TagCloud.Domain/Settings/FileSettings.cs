using Aspose.Drawing.Imaging;

namespace TagCloud.Domain.Settings;

public class FileSettings
{
    private string outFileName = "cloud";
    private string outPathToFile = "../../../TagCloudImages";
    private string fileFromWithPath = "../../../src/source.txt";

    public string OutFileName
    {
        get => outFileName;
        set
        {
            if (Path.GetInvalidFileNameChars().Any(value.Contains))
                throw new ArgumentException($"Name {value} is invalid");

            outFileName = value;
        }
    }

    public string OutPathToFile
    {
        get => outPathToFile;
        set
        {
            if (Path.GetInvalidPathChars().Any(value.Contains))
                throw new ArgumentException($"Path {value} is invalid");

            outPathToFile = value;
        }
    }

    public string FileFromWithPath
    {
        get => fileFromWithPath;
        set
        {
            if (!File.Exists(value))
                throw new ArgumentException($"There is no file by path {value}");

            fileFromWithPath = value;
        }
    }
    
    public ImageFormat ImageFormat { get; set; } = ImageFormat.Png;
}