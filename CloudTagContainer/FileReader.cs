using System.IO;

namespace Visualization
{
    public class FileReader: IFileReader
    {
        public string ReadToEnd(Stream inputSteam)
        {
            var textSteam = new StreamReader(inputSteam);
            return textSteam.ReadToEnd();
        }
    }
}