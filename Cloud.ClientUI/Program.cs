using CloudContainer;

namespace Cloud.ClientUI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var container = new TagCloudContainer();
            container.CreateTagCloud(args);
        }
    }
}