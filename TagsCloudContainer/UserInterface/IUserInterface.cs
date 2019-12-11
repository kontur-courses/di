using System;
using TagsCloudContainer.Core;

namespace TagsCloudContainer.UserInterface
{
    public interface IUserInterface
    {
        void Run(string[] programArgs, Action<Parameters> runProgram);
    }
}