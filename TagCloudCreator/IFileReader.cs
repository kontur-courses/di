namespace TagCloudCreator
{
    public interface IFileReader
    {
        string[] Types { get; }
        public string[] ReadAllLinesFromFile(string path);
    }
}