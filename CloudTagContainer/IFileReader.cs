using System.IO;

namespace Visualization
{
    public interface IFileReader
    {
        public string ReadToEnd(Stream inputSteam);
    }
}