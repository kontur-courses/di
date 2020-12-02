﻿using System;
using System.Drawing;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.BackgroundPainter;
using TagCloud.FrequencyAnalyzer;
using TagCloud.Layout;

namespace TagCloud
{
    class Program
    {
        private static IServiceProvider serviceProvider;
        private static CommandLineApplication app = new CommandLineApplication();
        static int Main(string[] args)
        {
            var CLI = new CommandLineInterface();
            CLI.ConfigureCLI(app);
            app.Execute(args);
            ConfigureServices(CLI.CanvasSize, CLI.BackgroundType);
            
            var visualizer = serviceProvider.GetService<IVisualizer>();
            visualizer.Visualize(CLI.FileName, CLI.StringFont, CLI.StringColor);

            return 0;
        }
        
        private static void ConfigureServices(Size size, Background background)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IPathCreater, PathCreater>();
            services.AddSingleton<IWordParser, LiteratureTextParser>();
            services.AddSingleton<IFrequencyAnalyzer, FrequencyAnalyzer.FrequencyAnalyzer>();
            services.AddSingleton<ICanvas>(_ => new Canvas(size.Width, size.Height));
            services.AddSingleton<ISpiral, Spiral>();
            services.AddSingleton<ILayouter, Layouter>();
            services.AddSingleton<ITagsCreater, TagsCreater>();
            ConfigureBackgroundPainterService(services, background);
            services.AddSingleton<IVisualizer, Visualizer>();

            serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureBackgroundPainterService(ServiceCollection services, Background background)
        {
            switch (background)
            {
                case Background.Circle:
                    services.AddSingleton<IBackgroundPainter, BackgroundPainterCircle>();
                    break;
                case Background.Empty:
                    services.AddSingleton<IBackgroundPainter, BackgroundPainterEmpty>();
                    break;
                case Background.Rectangles:
                    services.AddSingleton<IBackgroundPainter, BackgroundPainterRectangles>();
                    break;
            }
        }
    }
}