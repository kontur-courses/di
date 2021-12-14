using System.IO;

namespace Visualization.Readers
{
    public class TxtFileReader: IFileReader
    {
        public string ReadToEnd(Stream inputSteam)
        {
            var textSteam = new StreamReader(inputSteam);
            return textSteam.ReadToEnd();
        }
    }
}