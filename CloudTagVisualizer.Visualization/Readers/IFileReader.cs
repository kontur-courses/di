using System.IO;

namespace Visualization.Readers
{
    public interface IFileReader
    {
        public string ReadToEnd(Stream inputSteam);
    }
}