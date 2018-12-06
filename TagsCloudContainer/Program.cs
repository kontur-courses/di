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
            var inputPath = "hello.txt";
            if (ui.InputPath != null)
                inputPath = ui.InputPath;

            var words = ReadWords(inputPath);
            var blacklistPath = "blacklist.txt";
            if (ui.BlacklistPath != null)
                blacklistPath = ui.BlacklistPath;
            var tagsCloudCenter = new Point(800, 600);
            if (!ui.TagsCloudCenter.IsEmpty)
                tagsCloudCenter = ui.TagsCloudCenter;
            var letterSize = new Size(16, 20);
            if (!ui.LetterSize.IsEmpty)
                letterSize = ui.LetterSize;
            var cloud = CreateCloud(words, blacklistPath, tagsCloudCenter, letterSize);
            var outputPath = "output.png";
            if (ui.OutputPath != null)
                outputPath = ui.OutputPath;
            var color = Color.DarkBlue;
            if (!ui.TextColor.IsEmpty)
                color = ui.TextColor;
            var imageSize = new Size(1920, 1280);
            if (!ui.ImageSize.IsEmpty)
                imageSize = ui.ImageSize;
            RenderCloud(outputPath, cloud, FontFamily.GenericMonospace, color, imageSize);
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
            containerBuilder.RegisterType<IMGTagsCloudRenderer>().As<ITagsCloudRenderer>().WithParameters(
                new List<Parameter>()
                {
                    new NamedParameter("fontFamily", fontFamily),
                    new NamedParameter("textColor", textColor),
                    new NamedParameter("pictureSize", pictureSize)
                });


            var renderer = containerBuilder.Build().Resolve<ITagsCloudRenderer>();
            renderer.RenderIntoFile(outputPath, cloud);
        }
    }
}