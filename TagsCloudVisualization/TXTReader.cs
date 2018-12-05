using System.IO;
using System.Text;

namespace TagsCloudVisualization
{
    public class TxtReader : IFileReader
    {
        private readonly string filename;
        
        public TxtReader(string filename)
        {
            this.filename = filename;
        }

        public string ReadToEnd()
        {
            using (var streamReader = new StreamReader(filename, Encoding.UTF8))
                return streamReader.ReadToEnd();
        }
    }
}