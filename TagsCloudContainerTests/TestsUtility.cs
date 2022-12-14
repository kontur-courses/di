namespace CloudGeneratorTests;

public static class TestsUtility
{
    public static string GetFullPathFromRelative(string relativePath)
    {
        var sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var sFile = Path.Combine(sCurrentDirectory, relativePath);
        return Path.GetFullPath(sFile);
    }
}