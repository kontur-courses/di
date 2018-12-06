using System.Drawing;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Core;
using NSubstitute;

namespace TagsCloudVisualization
{
    public static class DependenciesBuilder
    {
        public static IContainer BuildContainer()
        {
            
            var builder = new ContainerBuilder();

            builder.RegisterType<TextPreprocessor>().As<TextPreprocessor>();
            builder.RegisterType<TxtReader>().As<IWordsProvider>()
                .WithParameter("filename", "examples/textExample.txt");
            builder.RegisterType<TxtReader>().Named<IWordsProvider>("boringWords")
                .WithParameter("filename", "examples/boring_words.txt");
            builder.RegisterType<BoringWordsFilter>().As<IFilter>()
                .WithParameter(ResolvedParameter.ForNamed<IWordsProvider>("boringWords"));
            builder.RegisterType<Spiral>().As<IPolar>()
                .WithParameters(new[]
                {
                    new NamedParameter("center", new Point(0, 0)),
                    new NamedParameter("step", 0.0005),
                    new NamedParameter("angle", 0)
                });
            builder.Register(c => new FontSettings(new FontFamily("Times New Roman"), FontStyle.Regular)).AsSelf();
            builder.Register(c => new Palette(Color.DimGray, Brushes.FloralWhite)).AsSelf();
            builder.Register(c => new Size(800, 800)).AsSelf();
            
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                .Except<TextPreprocessor>()
                .Except<TxtReader>()
                .Except<BoringWordsFilter>()
                .Except<Spiral>()
                .AsImplementedInterfaces();
            return builder.Build();
        }
    }
}