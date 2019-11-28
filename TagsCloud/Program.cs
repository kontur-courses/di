using System;
using System.IO;
using Autofac;
using TagsCloud.FileReader;
using TagsCloud.Interfaces;

namespace TagsCloud
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            try
            {
                var container = new ContainerBuilder();

                container.RegisterType<TxtReader>().As<IFileReader>();
                container.RegisterType<DefaultPathValidator>().As<IPathValidator>();

                Container = container.Build();

                Console.WriteLine(GetFileContent("123"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static string GetFileContent(string path)
        {
            using(var scope = Container.BeginLifetimeScope())
            {
                var reader = Container.Resolve<IFileReader>();
                var fileStream = reader.ReadFile(path);
                return new StreamReader(fileStream).ReadToEnd();
            }
        }
    }
}
