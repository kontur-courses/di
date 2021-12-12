using System;
using System.IO;
using System.Text;

namespace TagCloud_ConsoleUI
{
    public class ConsoleStatusWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public override void WriteLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}