using System.IO;

namespace CloudTagContainer
{
    public interface IWordsReader
    {
        public string[] Read(Stream inputStream);
    }
}