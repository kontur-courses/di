using LightInject;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerProvider.GetContainer();
            var ui = container.GetInstance<ConsoleUI>();
            ui.Run();
        }
    }
}
