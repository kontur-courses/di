using System;

namespace TagsCloud.Writer
{
    public class ConsoleWriter: IWriter
    {
        public void Write(string str)
        {
            Console.WriteLine(str);
        }
    }
}