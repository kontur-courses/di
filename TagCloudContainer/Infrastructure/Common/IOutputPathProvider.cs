namespace TagCloud.Infrastructure.Common;

public interface IOutputPathProvider
{
    string OutputPath { get; }
    public string OutputFormat { get; }
}