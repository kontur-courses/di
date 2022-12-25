using Autofac;
using CommandLine;
using TagsCloudContainer.Core.CLI;
using TagsCloudContainer.Core.Drawer;
using TagsCloudContainer.Core.Options;
using TagsCloudContainer.Core.Layouter;
using TagsCloudContainer.Core.TagsClouds;
using TagsCloudContainer.Core.WordsParser;
using TagsCloudContainer.Core.Drawer.Interfaces;
using TagsCloudContainer.Core.Options.Interfaces;
using TagsCloudContainer.Core.Layouter.Interfaces;
using TagsCloudContainer.Core.TagsClouds.Interfaces;
using TagsCloudContainer.Core.WordsParser.Interfaces;

namespace TagsCloudContainer.Sandbox
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var options = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;
            var tagCloudContainer = ConfigureTagsCloudContainer(options);

            tagCloudContainer.CreateTagCloud();
            tagCloudContainer.SaveTagCloud();
        }

        private static ITagsCloud ConfigureTagsCloudContainer(CommandLineOptions options)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ImageOptions>().As<IImageOptions>()
                .WithParameters(new[]
                {
                    new NamedParameter("width", options.Width),
                    new NamedParameter("height", options.Height),
                    new NamedParameter("imageOutputDirectory", options.OutputDirectory),
                    new NamedParameter("imageName", options.OutputFileName),
                    new NamedParameter("imageExtension", options.OutputFileExtension)
                });
            builder.RegisterType<FontOptions>()
                .WithParameters(new[]
                {
                    new NamedParameter("fontFamily", options.FontFamily),
                    new NamedParameter("fontColor", options.FontColor)
                });
            builder.RegisterType<FilterOptions>().As<IFilterOptions>()
                .WithParameters(new[]
                {
                    new NamedParameter("myStemLocation", options.MystemLocation),
                    new NamedParameter("boringWords", options.BoringWords)
                });
            builder.RegisterType<WordsAnalyzer>().As<IWordsAnalyzer>();
            builder.RegisterType<WordsReader>().As<IWordsReader>().WithParameter("filePath", options.FilePath);
            builder.RegisterType<WordsFilter>().As<IWordsFilter>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<LayoutDrawer>().As<ILayoutDrawer>();
            builder.RegisterType<RectangleLayout>().As<IRectangleLayout>();
            builder.RegisterType<TagsCloud>().As<ITagsCloud>();

            var container = builder.Build();

            return container.Resolve<ITagsCloud>();
        }
    }
}