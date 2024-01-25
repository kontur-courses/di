namespace TagCloud.Domain.Settings;

public class PathSettings
{
    private string fileName = "cloud.png";
    private string pathToFile = "../../../TagCloudImages";

    public string FileName
    {
        get => fileName;
        set
        {
            if (Path.GetInvalidFileNameChars().Any(value.Contains))
                throw new ArgumentException($"Name {value} is invalid");

            fileName = value;
        }
    }

    public string PathToFile
    {
        get => pathToFile;
        set
        {
            if (Path.GetInvalidPathChars().Any(value.Contains))
                throw new ArgumentException($"Path {value} is invalid");

            pathToFile = value;
        }
    }

    public string FileFrom => "../../../src/source.txt";
}