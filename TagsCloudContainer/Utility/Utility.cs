namespace TagsCloudContainer.utility;

public static class Utility
{
    public static string GetAbsoluteFilePath(string fileName)
    {
        if (fileName.Contains('/') &&
            !Directory.Exists($"../../../../TagsCloudContainer/{fileName[..fileName.LastIndexOf('/')]}"))
            Directory.CreateDirectory($"../../../../TagsCloudContainer/{fileName[..fileName.LastIndexOf('/')]}");
        return $"../../../../TagsCloudContainer/{fileName}";
    }
}