using System.IO;

namespace Visualization
{
    public interface IWordsParser
    {
        public string[] Read(string fullString);
    }
}