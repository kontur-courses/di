using System;

namespace TagCloud.App.CLI
{
    public class ConsoleInputEventArgs : EventArgs
    {
        public ConsoleInputEventArgs(string input, bool isTransfer)
        {
            IsTransfer = isTransfer;
            Input = input;
        }

        public bool IsTransfer { get; }

        public string Input { get; }
    }
}