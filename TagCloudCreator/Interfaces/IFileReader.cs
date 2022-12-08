namespace TagCloudCreator.Interfaces;

public interface IFileReader
{
    public string SupportedExtension { get; }
    string ReadFile();
}