using LightInject;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerProvider.GetContainer();
            var ui = container.GetInstance<IUI>();
            ui.Run();
        }
    }
}
