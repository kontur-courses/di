using System.IO;

namespace TagsCloudContainer
{
    public class PreparedFile : ISource
    {
        private readonly string filename;

        public PreparedFile(string filename)
        {
            this.filename = filename;
        }

        public string[] Parse()
        {
            return File.ReadAllLines(filename);
        }
    }
}
