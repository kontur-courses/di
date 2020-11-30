using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloud.Commands
{
    public interface ICommand
    {
        string CommandId { get; }
        string Description { get; }
        void Handle(string[] args);
    }
}
