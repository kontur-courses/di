using TagsCloudContainer.Settings;

namespace TagsCloudContainer;

public class FileReader
{
    private readonly CloudData cloudData;

    public FileReader(CloudData cloudData)
    {
        this.cloudData = cloudData;
    }

    public string ReadFile()
    {
        return File.ReadAllText(cloudData.FilePath);
    }
}