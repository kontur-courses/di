using System;

namespace TagCloud.App.CLI
{
    public class ConsoleInputEventArgs : EventArgs
    {
        public ConsoleInputEventArgs(string input)
        {
            Input = input;
        }

        public string Input { get; }
    }
}