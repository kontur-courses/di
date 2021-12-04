namespace TagCloudContainer.Infrastructure.Common;

public interface IOutputPathProvider
{
    string OutputPath { get; }
    public string OutputFormat { get; }
}