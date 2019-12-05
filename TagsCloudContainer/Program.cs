using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TagsCloudContainer.Parsing;
using TagsCloudContainer.RectangleTranslation;
using TagsCloudContainer.Word_Counting;

namespace TagsCloudContainer
{
    public class Program
    {
        private static IContainer container;
        public static void Main(string[] args)
        {
            SetDependencies();
            using (var scope = container.BeginLifetimeScope())
            {
                var layouter = scope.Resolve<ICloudLayouter>();
                var a = new TxtParser();
                var c = a.ParseFile("example.txt");

                foreach (var v in c)
                {
                    Console.WriteLine(c);
                }
            }
        }

        private static void SetDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TxtParser>().As<IFileParser>();
            builder.RegisterType<RectangleTranslator>().As<IRectangleTranslator>();
            builder.RegisterType<WordFilter>().As<IWordFilter>();
            builder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            builder.RegisterType<WordCounter>().As<IWordCounter>();
            builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();
            container = builder.Build();
        }
    }
}
