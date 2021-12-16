using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using TagsCloudVisualization;
using TagsCloudVisualization.Factories;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.PointPlacers;
using TagsCloudVisualization.TextHandlers;
using TagsCloudVisualization.TextPreparers;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.Visualization.Configurator;

namespace TagsCloudVisualizationTests
{
    internal class ContainerProvider
    {
        public IContainer Container { get; private set; }
        private ContainerBuilder builder;

        public ContainerProvider()
        {
            builder = new ContainerBuilder();
        }

        public void RegisterDependencies()
        {
            builder.RegisterType<Spiral>().As<IPointPlacer>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<WordsVisualizer>().As<IVisualizer>();
            builder.RegisterType<WordsVisualizingTokenFactory>().As<IVisualizingTokenFactory>();
            builder.RegisterType<WordsVisualizingConfigurator>().As<IVisualizingConfigurator>();
            builder.RegisterType<TxtParser>().As<IParser>();
            builder.RegisterType<TextPreparer>().As<ITextPreparer>();
            builder.RegisterType<TextHandler>().As<ITextHandler>();
            builder.RegisterType<TagCloudCreator>().As<ITagCloudCreator>();
            
            var screenConfig = new ScreenConfig {BackgroundColor = Color.White, Size = new Size(800, 600)};
            var center = new Point(400, 300);
            var fontSize = 11;
            Func<string, Color> colorizer = _ => Color.Black;
            var wordsPreparers = new List<Func<string, string>> {s => s.ToLower()};
            var wordsFilters = new List<Func<string, bool>> {s => s.Length < 3};
            
            builder.Register(_ => screenConfig).As<ScreenConfig>();
            builder.Register(_ => center).As<Point>();
            builder.Register(_ => fontSize).As<int>();
            builder.RegisterInstance(colorizer);
            builder.RegisterInstance(wordsPreparers).As<IEnumerable<Func<string, string>>>();
            builder.RegisterInstance(wordsFilters).As<IEnumerable<Func<string, bool>>>();

            Container = builder.Build();
        }
    }
}