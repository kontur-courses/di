using System.IO;

namespace TagsCloudContainer
{
    public class PreparedSourceFile : ISource
    {
        private readonly string filename;

        public PreparedSourceFile(string filename)
        {
            this.filename = filename;
        }

        public string[] GetWords()
        {
            return File.ReadAllLines(filename);
        }
    }
}
