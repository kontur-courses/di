namespace TagsCloudVisualizationConsole;

public static class AppOptionsValidator
{
    public static void ValidatePathsInOptions(ArgsOptions? argsOptions)
    {
        if (argsOptions == null)
            throw new ArgumentNullException(nameof(argsOptions));

        if (!File.Exists(argsOptions.PathToTextFile))
            throw new ArgumentException($"{argsOptions.PathToTextFile} does not exist");

        if (!Directory.Exists(argsOptions.DirectoryToSaveFile))
            throw new ArgumentException($"{argsOptions.DirectoryToSaveFile} does not exist");

        if (string.IsNullOrEmpty(argsOptions.SaveFileName))
            throw new ArgumentException($"Save file name empty");
    }
}