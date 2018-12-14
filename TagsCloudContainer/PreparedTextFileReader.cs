using System.IO;

namespace TagsCloudContainer
{
    public class PreparedTextFileReader : ISource
    {
        private readonly string filename;

        public PreparedTextFileReader(string filename)
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
