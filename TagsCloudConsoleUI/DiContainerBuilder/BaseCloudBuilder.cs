using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using Autofac.Core;
using SyntaxTextParser;
using TagsCloudGenerator;
using TagsCloudGenerator.CloudPrepossessing;

namespace TagsCloudConsoleUI.DiContainerBuilder
{
    internal static class CloudBuilder
    {
        private static readonly IReadOnlyDictionary<string, Func<BuildOptions, IContainer>> PresetsLists =
            new Dictionary<string, Func<BuildOptions, IContainer>>
            {
                ["YandexCircularRandomImage"] = options => 
                    BuildPreset(new CircularRandomCloudModule(options),
                                new WordParserWithYandexToolModule(options),
                                new BitmapImageCreatorModule(options)),
            };

        private static IContainer BuildPreset(params IModule[] modules)
        {
            var builder = new ContainerBuilder();

            foreach (var module in modules)
                builder.RegisterModule(module);

            return builder.Build();
        }

        private static IContainer GetContainer(BuildOptions options)
        {
            var name = options.CloudPreset;

            if (PresetsLists.ContainsKey(name))
                return PresetsLists[name](options);
            throw new ArgumentException($"Cloud builder don't have preset - {name}");
        }

        public static void CreateCloudImageAndSave(BuildOptions options)
        {
            var presetContainer = GetContainer(options);

            var text = presetContainer.Resolve<TextParser>()
                .ParseElementsFromFile(options.InputFileName);

            var cloudConfig = presetContainer
                .Resolve<CloudFormat>(new NamedParameter("tagTextFontFamily", options.FontFamily),
                    new NamedParameter("fontSizeMultiplier", options.FontSizeMultiplier),
                    new NamedParameter("maximalFontSize", options.MaximalFontSize));

            var cloudLayout = presetContainer.Resolve<ITagsPrepossessing>();

            var cloudTags = CloudGenerator.CreateTagsCloud(text, cloudLayout, cloudConfig);

            var bitmapImage = presetContainer.Resolve<ICloudBuilder<Bitmap>>()
                .CreateTagCloudFromTags(cloudTags, new Size(options.Width, options.Height), cloudConfig);

            bitmapImage.Save(options.OutputFileName, ImageFormatter.ParseImageFormat(options.ImageExtension));
        }
    }
}