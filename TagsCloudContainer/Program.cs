using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization;
using Autofac;
using Autofac.Core;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<CLI>().As<IUI>().WithParameter("args", args);
            var ui = cb.Build().Resolve<IUI>();
            var words = ReadWords(ui.InputPath);
            var cloud = CreateCloud(words, ui.BlacklistPath, ui.TagsCloudCenter, ui.LetterSize);
            RenderCloud(ui.OutputPath, cloud, FontFamily.GenericMonospace, ui.TextColor, ui.ImageSize);
        }


        public static List<string> ReadWords(string inputPath)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<TxtWordsReader>().As<IWordsReader>();
            var parser = containerBuilder.Build().Resolve<IWordsReader>();
            return parser.ReadWords(inputPath);
        }

        public static ITagsCloud CreateCloud(List<string> words, string blacklistPath, Point center, Size minLetterSize)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<BlacklistWordsFilter>().As<IWordsFilter>().WithParameter
                ("blacklistPath", blacklistPath);
            containerBuilder.RegisterType<ToLowerCaseFormatter>().As<IWordsFormatter>();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<ITagsCloudLayouter>()
                .WithParameter("center", center);
            containerBuilder.RegisterType<TagsCloudGenerator>().AsSelf()
                .WithParameter("minLetterSize", minLetterSize);
            var generator = containerBuilder.Build().Resolve<TagsCloudGenerator>();
            return generator.CreateCloud(words);
        }

        private static void RenderCloud
            (string outputPath, ITagsCloud cloud, FontFamily fontFamily, Color textColor, Size pictureSize)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ImageSettings>().AsSelf().WithParameters(new List<Parameter>()
            {
                new NamedParameter("fontFamily", fontFamily),
                new NamedParameter("textColor", textColor),
                new NamedParameter("pictureSize", pictureSize)
            }).SingleInstance();

            containerBuilder.RegisterType<PNGTagsCloudRenderer>().As<ITagsCloudRenderer>();
            var renderer = containerBuilder.Build().Resolve<ITagsCloudRenderer>();
            renderer.RenderIntoFile(outputPath, cloud);
        }
    }
}