using Autofac;
using TagsCloudContainer.Application;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var container = Container.SetDIBuilder();
            var app = container.Resolve<IApp>();
            app.Run();
        }
    }
}