using System;

namespace TagCloud.App.CLI
{
    public class ConsoleInputEventArgs : EventArgs
    {
        public bool IsTransfer { get; }

        public ConsoleInputEventArgs(string input, bool isTransfer)
        {
            this.IsTransfer = isTransfer;
            Input = input;
        }

        public string Input { get; }
    }
}