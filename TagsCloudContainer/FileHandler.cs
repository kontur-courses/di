namespace TagsCloudContainer;

public static class FileHandler
{
    public static string ReadText(string fileName)
    {
        return File.ReadAllText(GetRelativeFilePath($"src/{fileName}.txt"));
    }

    public static string GetRelativeFilePath(string fileName)
    {
        if (fileName.Contains('/') && !Directory.Exists($"../../../../TagsCloudContainer/{fileName[..fileName.LastIndexOf('/')]}"))
            Directory.CreateDirectory($"../../../../TagsCloudContainer/{fileName[..fileName.LastIndexOf('/')]}");
        return $"../../../../TagsCloudContainer/{fileName}";
    }
}