namespace TagCloud.App.UI.Common;

public interface IOutputPathProvider
{
    string OutputPath { get; }
    public string OutputFormat { get; }
}