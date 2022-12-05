namespace TagsCloudVisualization
{
    internal class FileParserForLines
    {
        private readonly string path;

        public FileParserForLines(string path)
        {
            this.path = path;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(/*Environment.CurrentDirectory + "\\..\\..\\..\\" +*/ path);
        }
    }
}