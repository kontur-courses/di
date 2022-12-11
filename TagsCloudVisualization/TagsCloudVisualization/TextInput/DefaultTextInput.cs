namespace TagsCloudVisualization.TextInput;

public class DefaultTextInput : ITextInput
{
    public string GetInputString(string path = Config.DefaultPath)
    {
        if (!File.Exists(path))
            throw new Exception("File doesn't exist");

        return File.ReadAllText(path);
    }
}