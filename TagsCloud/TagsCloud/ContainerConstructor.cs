using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using Autofac.Core;
using TagsCloud.CloudConstruction;
using TagsCloud.Visualization;
using TagsCloud.Visualization.ColorDefiner;
using TagsCloud.Visualization.SizeDefiner;
using TagsCloud.WordPreprocessing;
using TagsCloud.Writer;

namespace TagsCloud
{
    public static class ContainerConstructor
    {
        public static IContainer Configure(IEnumerable<string> args)
        {
            var options = Options.Parse(args);
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameter("center", new Point())
                .SingleInstance();
            builder.RegisterType<Layouter>().As<ILayouter>();
            builder.RegisterType<Visualizer>().As<IVisualizer>().WithParameters(new List<Parameter>
            {
                new NamedParameter("fontName", options.Font),
                new NamedParameter("backgroundColor", Color.FromName(options.BackgroundColor)),
            });
            builder.RegisterType<FileReader>().As<IWordGetter>().WithParameter("fileName", new FileInfo(options.File));
            builder.RegisterType<WordStatisticGetter>().As<IWordAnalyzer>();
            builder.RegisterType<RandomColorDefiner>().As<IColorDefiner>();
            builder.RegisterType<FrequencySizeDefiner>().As<ISizeDefiner>().WithParameters(new List<Parameter>
                {
                    new NamedParameter("fontName", options.Font),
                    new NamedParameter("minFontSize", options.MinFontSize),
                    new NamedParameter("maxFontSize", options.MaxFontSize)
                }
            );
            builder.RegisterType<ConsoleWriter>().As<IWriter>();
            builder.RegisterType<WordsCleaner>().As<IWordsProcessor>().WithParameter("infinitive", options.Infinitive);
            builder.RegisterType<Application>().AsSelf().WithParameters(new List<Parameter>
            {
                new NamedParameter("options", options),
                new NamedParameter("imageFormat", options.Format),
            });

            return builder.Build();
        }
    }
}