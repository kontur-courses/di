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
            if (filename == null)
                return new string[0];

            return File.ReadAllLines(filename);
        }
    }
}
