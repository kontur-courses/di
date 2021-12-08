using System.Collections.Generic;

namespace TagCloud.Words.Reading.Console
{
    public class ConsoleReader : IConsoleReader
    {
        public IEnumerable<string> ReadFromConsole()
        {
            var line = System.Console.ReadLine();
            while (line != "")
            {
                yield return line;
                line = System.Console.ReadLine();
            }
        }
    }
}