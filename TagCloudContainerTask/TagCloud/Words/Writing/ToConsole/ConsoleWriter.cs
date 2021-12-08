using System;
using System.Collections.Generic;

namespace TagCloud.Words.Writing.ToConsole
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void TypeToConsole()
        {
            var lines = new List<string>();
            var line = Console.ReadLine();

            while (!string.IsNullOrEmpty(line))
            {
                lines.Add(line);
                line = Console.ReadLine();
            }

            lines.ForEach(element => Console.WriteLine(element));
        }

        public void WriteToConsole(IEnumerable<string> linesToWrite)
        {
            foreach (var item in linesToWrite) Console.WriteLine(item);
        }
    }
}