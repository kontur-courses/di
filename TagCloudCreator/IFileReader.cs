namespace TagCloudCreator
{
    public interface IFileReader
    {
        public string[] ReadAllLinesFromFile(string path);
    }
}