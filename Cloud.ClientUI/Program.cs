using CloudContainer;

namespace Cloud.ClientUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = new TagCloudContainer();
            container.Run(args);
        }
    }
}