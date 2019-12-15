using Autofac;
using Autofac.Core;
using SyntaxTextParser;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using TagsCloudGenerator;
using TagsCloudGenerator.CloudPrepossessing;

namespace TagsCloudConsoleUI
{
    internal static class CloudBuilder
    {
        public static IContainer BuildPreset(params IModule[] modules)
        {
            var builder = new ContainerBuilder();

            foreach (var module in modules)
                builder.RegisterModule(module);

            return builder.Build();
        }

        public static ImageFormat ParseImageFormat(string str)
        {
            return (ImageFormat) typeof(ImageFormat)
                .GetProperty(str, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
                ?.GetValue(str, null);
        }

        public static void CreateCloudImageAndSave(BuildOptions options, IContainer presetContainer)
        {
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

            bitmapImage.Save(options.OutputFileName, ParseImageFormat(options.ImageExtension));
        }
    }
}