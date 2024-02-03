﻿using Autofac;
using TagsCloud.App;
using TagsCloud.ColorGenerators;
using TagsCloud.ConsoleCommands;
using TagsCloud.Distributors;
using TagsCloud.Layouters;
using TagsCloud.TagsCloudPainters;
using TagsCloud.WordsProviders;
using TagsCloud.WordFontCalculators;
using TagsCloud.WordValidators;


namespace TagsCloud;

public static class ContainerConfig
{
    public static IContainer Configure(Options options)
    {
        var builder = new ContainerBuilder();
        builder.RegisterInstance(options).AsSelf();
        builder.RegisterType<RandomColorGenerator>().As<IColorGenerator>().SingleInstance();
        builder.RegisterType<SpiralDistributor>().As<IDistributor>();
        builder.RegisterType<SimplePainter>().As<IPainter>();
        builder.RegisterType<WordsProvider>().As<IWordsProvider>();
        builder.RegisterType<SimpleWordFontCalculator>().As<IWordFontCalculator>();
        builder.RegisterType<SimpleWordValidator>().As<IWordValidator>();
        builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
        builder.RegisterType<App.App>().As<IApp>();
        return builder.Build();
    }
}