using System.Drawing;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Visualizer;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization.Infrastructure
{
    public class DIBuilder
    {
        private readonly IContainer container;
        private readonly Options options;

        public DIBuilder(Options options)
        {
            this.options = options;
            container = BuildContainer();
        }

        public ISaver<IVisualizer<IWordsCloudBuilder>> Resolve()
        {
            using (var scope = container.BeginLifetimeScope())
            {
               return scope.Resolve<ISaver<IVisualizer<IWordsCloudBuilder>>>();
            }
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TxtReader>().Named<IWordsProvider>("wordsFile")
                .WithParameter("filename", options.WordsFilename);
            builder.RegisterType<TxtReader>().Named<IWordsProvider>("boringWords")
                .WithParameter("filename", options.BoringWordsFile);
            builder.RegisterType<TextPreprocessor>().As<IWordsProvider>()
                .WithParameter(ResolvedParameter.ForNamed<IWordsProvider>("wordsFile"));
            builder.RegisterType<BoringWordsFilter>().As<IFilter>()
                .WithParameter(ResolvedParameter.ForNamed<IWordsProvider>("boringWords"));
            builder.RegisterType<BaseFormConverter>().As<IWordsChanger>()
                .WithParameters(new[]
                {
                    new NamedParameter("affFileName", options.DictFileNames.ElementAt(0)),
                    new NamedParameter("dicFileName", options.DictFileNames.ElementAt(1)), 
                });

            builder.RegisterType<Spiral>().As<Spiral>()
                .WithParameters(new[]
                {
                    new NamedParameter("center", new Point(options.CentralPoint.ElementAt(0), options.CentralPoint.ElementAt(1))),
                    new NamedParameter("step", options.Step),
                    new NamedParameter("angle", options.Angle)
                });
            builder.Register(c => new FontSettings(new FontFamily(options.FontFamily), (FontStyle)options.FontStyle, options.MaxFontSize)).AsSelf();
            builder.Register(c => new Palette(Color.FromName(options.BackgroundColor), new SolidBrush(Color.FromName(options.ForegroundColor)))).AsSelf();
            builder.Register(c => new Size(options.Size.ElementAt(0), options.Size.ElementAt(1))).AsSelf();
            
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                .Except<TextPreprocessor>()
                .Except<TxtReader>()
                .Except<BoringWordsFilter>()
                .Except<Spiral>()
                .Except<BaseFormConverter>()
                .AsImplementedInterfaces();
            return builder.Build();
        }
    }
}