using System.IO;

namespace Visualization
{
    public interface IWordsReader
    {
        public string[] Read(Stream inputStream);
    }
}