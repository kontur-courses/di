using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using CommandLine;
using TagsCloud;

namespace TagCloudConsole
{
    public static class EntryPoint
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleErrors);
        }

        private static void Run(Options options)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TagCloudRender>().AsSelf();
            containerBuilder.RegisterInstance(
                new WithoutBoringWordCollection(
                    new LowerWordCollection(
                        new WordsFromFile(options.BoringWordsPath)
                    ),
                    new LowerWordCollection(
                        new WordsFromFile(options.TextPath)
                    )
                )
            ).As<IWordCollection>();
            containerBuilder.RegisterInstance(new CoordinatesAtImage(options.Name, options.FontFamily,
                Color.FromName(options.Color),
                new Size(options.Height, options.Width))).AsSelf();
            containerBuilder.RegisterType<CreateLayout>().AsSelf();
            containerBuilder.RegisterType<FrequencyDictionary>().AsSelf();
            containerBuilder.RegisterInstance(new CircularCloudLayouter(new Point(0, 0))).As<ICloudLayouter>();
            using (var container = containerBuilder.Build())
            {
                var render = container.Resolve<TagCloudRender>();
                render.Render();
            }
        }

        private static void HandleErrors(IEnumerable<Error> errors)
        {
            Console.WriteLine(
                string.Join(
                    Environment.NewLine,
                    errors
                )
            );
        }
    }
}