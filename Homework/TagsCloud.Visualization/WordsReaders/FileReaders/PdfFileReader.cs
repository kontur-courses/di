namespace TagsCloud.Visualization.WordsReaders.FileReaders
{
    public class PdfFileReader : IFileReader
    {
        public string Extension => "pdf";
        public string Read(string filename) => throw new System.NotImplementedException();
    }
}