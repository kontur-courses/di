using System;
using TagsCloudVisualization;
using System.Drawing;
using Autofac;
using TagsCloudVisualization.CloudGenerating;
using TagsCloudVisualization.Preprocessors;

namespace TagsCloudConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new CustomParser();
            CustomArgs arguments;
            try
            {
                arguments = parser.Parse(args);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            

            ImageSettings imageSettings = new ImageSettings()
            {
                BackgroundColor = arguments.BackgroundColor,
                TextColor = arguments.TextColor,
                FontName = arguments.FontName,
                ImageSize = arguments.ImageSize
            };

            var builder = new ContainerBuilder();
            builder.RegisterType<TextFileReader>()
                .As<IReader>()
                .WithParameter("fileName", arguments.WordsFile);

            builder.RegisterInstance(imageSettings).As<ImageSettings>();
            builder.RegisterType<CircularCloudLayouter>()
                .As<ILayouter>()
                .WithParameter("center", new Point(0, 0));
            
            builder.RegisterType<DullWordsFilter>().As<IFilter>();
            builder.RegisterType<BasicTransformer>().As<IWordTransformer>();
            builder.RegisterType<TagsCloudGenerator>().AsSelf();
            builder.RegisterType<CloudBuilder>().AsSelf();
            builder.RegisterType<TagsCloudVisualizer>().AsSelf();
            var container = builder.Build();

            var reader = container.Resolve<IReader>();
            var words = reader.ReadAllWords();

            var cloudBuilder = container.Resolve<CloudBuilder>();
            var tagCloud = cloudBuilder.BuildTagCloud(words);
            var visualizer = container.Resolve<TagsCloudVisualizer>();

            var picture = visualizer.GetPictureOfRectangles(tagCloud);
            picture.Save(arguments.OutputFileName);
        }
    }
}
