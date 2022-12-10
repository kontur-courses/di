namespace TagsCloudVisualization.TextInput;

public class DefaultTextInput : ITextInput
{
    private string path;

    public DefaultTextInput(string path)
    {
        this.path = path;
    }

    public string GetInputString()
    {
        if (!File.Exists(path))
            throw new Exception("File doesn't exist");

        return File.ReadAllText(path);
    }
}