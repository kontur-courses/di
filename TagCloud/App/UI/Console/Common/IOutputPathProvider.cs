namespace TagCloud.App.UI.Console.Common;

public interface IOutputPathProvider
{
    string OutputPath { get; }
    public string OutputFormat { get; }
}