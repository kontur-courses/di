using Autofac;
using TagsCloudContainer.ResultFormatters;

namespace TagsCloudContainer
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            Container = new Ioc().GetContainer();

            using (var scope = Container.BeginLifetimeScope())
            {
                scope.Resolve<IResultFormatter>().GenerateResult("tag-cloud.png");
            }
        }
    }
}
