namespace TagsCloud.Visualization.WordsReaders.FileReaders
{
    public class TxtFileReader : IFileReader
    {
        public string Extension => "txt";
        public string Read(string filename) => throw new System.NotImplementedException();
    }
}