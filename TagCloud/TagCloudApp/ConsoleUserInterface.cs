using System;
using System.Collections.Generic;
using TagCloudCreation;

namespace TagCloudApp
{
    internal class ConsoleUserInterface : UserInterface
    {
        public ConsoleUserInterface(TagCloudCreator creator, IEnumerable<ITextReader> readers) : base(creator, readers)
        {
        }

        public override void Run(string[] startupArgs)
        {
            throw new NotImplementedException();
        }
    }
}
