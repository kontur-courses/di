using System;
using TagsCloudContainer.UserInterface;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var ui = new ConsoleUserInterface();
            if (ui.TryGetParameters(args, out var parameters))
            {
                Console.WriteLine(parameters);
            }
        }
    }
}
