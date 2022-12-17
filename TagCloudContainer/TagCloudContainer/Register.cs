﻿using Autofac;

namespace TagCloudContainer;

public static class Register
{
    public static IContainer Registry()
    {
        var builder = new ContainerBuilder();
        var mainFormConfig = new MainFormConfig();

        builder.RegisterType<TagCloudForm>();
        builder.RegisterType<Settings>();

        builder.RegisterInstance<IMainFormConfig>(mainFormConfig);
        builder.RegisterType<ImageCreator>().As<IImageCreator>().SingleInstance();
        builder.RegisterType<WordConfig>().As<IWordConfig>().SingleInstance();
        builder.RegisterType<TagConfig>().As<ITagConfig>().SingleInstance();
        builder.RegisterType<WordsReader>().As<IWordsReader>().SingleInstance();
        builder.RegisterType<TagCloudProvider>().As<ITagCloudProvider>().SingleInstance();
        
        return builder.Build();
    }
}