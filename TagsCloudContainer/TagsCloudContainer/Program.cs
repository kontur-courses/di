﻿using Autofac;
using Autofac.Features.ResolveAnything;
using DeepMorphy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var boringWords = new List<string>();
            boringWords.AddRange(BoringWords.Prepositions);
            boringWords.AddRange(BoringWords.Pronouns);

            var builder = new ContainerBuilder();

            builder.RegisterInstance(new LineByLineWordReader()).As<IWordReader>();

            builder.Register(cont => boringWords.ToHashSet()).As<HashSet<string>>();
            builder.RegisterType<ProcessorNonBoringWordsToLower>().As<IWordProcessor>();

            builder.RegisterType<TextFileWordsContainer>()
                .As<IWordsContainer>()
                .WithParameter("path", @"../../../../test.txt");

            builder.Register(p => new Point()).As<Point>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();

            builder.RegisterType<WordCloudCreator>().As<IWordCloudCreator>();
            builder.RegisterInstance(new ConsoleClient()).As<IClient>();
            builder.RegisterType<WordCloudPainter>().As<IWordCloudPainter>();

            builder.Register(cont => new ImageSaver(@"../../../../")).As<IImageSaver>();
            builder.RegisterType<WordCloudSaver>().As<IWordCloudSaver>();

            builder.RegisterType<ClientControl>().AsSelf();

            builder.RegisterType<App>().AsSelf();

            var container = builder.Build();

            var app = container.Resolve<App>();
            app.Start();
        }
    }
}
