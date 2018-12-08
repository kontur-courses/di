using System.Drawing;
using System.Reflection;
using Autofac;
using Autofac.Core;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization.Infrastructure
{
    public class DIBuilder
    {
        private readonly IContainer container;
        public DIBuilder()
        {
            container = BuildContainer();
        }

        public T Resolve<T>()
        {
            using (var scope = container.BeginLifetimeScope())
            {
               return scope.Resolve<T>();
            }
        }

        private static IContainer BuildContainer()
        {
            
            var builder = new ContainerBuilder();
            
            builder.RegisterType<TxtReader>().Named<IWordsProvider>("wordsFile")
                .WithParameter("filename", "examples/textExample.txt");
            builder.RegisterType<TxtReader>().Named<IWordsProvider>("boringWords")
                .WithParameter("filename", "examples/boring_words.txt");
            builder.RegisterType<TextPreprocessor>().As<IWordsProvider>()
                .WithParameter(ResolvedParameter.ForNamed<IWordsProvider>("wordsFile"));
            builder.RegisterType<BoringWordsFilter>().As<IFilter>()
                .WithParameter(ResolvedParameter.ForNamed<IWordsProvider>("boringWords"));

            builder.RegisterType<Spiral>().As<Spiral>()
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