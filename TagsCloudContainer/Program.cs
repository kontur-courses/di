using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Autofac;
using TagsCloudContainer.Common;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Layouters;
using TagsCloudContainer.Painting;
using TagsCloudContainer.Preprocessors;
using TagsCloudContainer.UI;
using TagsCloudContainer.UI.Menu;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            var preContainer = RegisterDependensiesInPreContainer(builder).Build();
            new Registrator(builder).RegisterDependencies();
            SetUpUi(preContainer);
        }

        internal static void Visualize(IContainer container)
        {
            var minHeight = 12;
            var maxScale = 10;
            using (var scope = container.BeginLifetimeScope())
            {
                var reader = scope.Resolve<TagReader>();
                var parser = scope.Resolve<WordsCountParser>();
                var preprocessor = scope.Resolve<TagsPreprocessor>();
                var layouter = scope.Resolve<TagLayouter>();
                var painter = scope.Resolve<TagPainter>();
                var visualizator = scope.Resolve<IVisualizator<ITag>>();
                var settings = ResolveSettings(scope);

                var text = reader.Read(AppSettings.TextFilename);
                var tags = parser.Parse(text);
                tags = preprocessor.Process(tags);
                var cloud = layouter.PlaceTagsInCloud(tags, minHeight, maxScale);
                painter.SetPalettes(cloud.Elements);
                visualizator.Visualize(settings, cloud);
            }
        }

        private static IVisualizatorSettings ResolveSettings(ILifetimeScope scope)
        {
            var settingParams = GetVisualizatorSettingsParams()
                .ToContainerParameters();
            return scope.Resolve<IVisualizatorSettings>(settingParams);
        }

        private static Dictionary<string, object> GetVisualizatorSettingsParams()
        {
            var dict = new Dictionary<string, object>();
            dict["filename"] = AppSettings.ImageFilename;
            if(AppSettings.ImageSize != new Size())
                dict["bitmapSize"] = AppSettings.ImageSize;
            if (AppSettings.BackgroundColor != new Color())
                dict["backgroundColor"] = AppSettings.BackgroundColor;
            if (AppSettings.FontFamily != null)
                dict["family"] = AppSettings.FontFamily;
            if (AppSettings.MinMargin != 0)
                dict["minMargin"] = AppSettings.MinMargin;
            if (AppSettings.FillTags)
                dict["fillTags"] = AppSettings.FillTags;
            return dict;
        }

        private static ContainerBuilder RegisterDependensiesInPreContainer(ContainerBuilder builder)
        {
            var preBuilder = new ContainerBuilder();
            preBuilder.RegisterInstance(Console.Out).As<TextWriter>();
            preBuilder.RegisterInstance(Console.In).As<TextReader>();
            preBuilder.RegisterInstance(builder).As<ContainerBuilder>();
            RegisterActions(preBuilder);
            return preBuilder;
        }

        private static void SetUpUi(IContainer preContainer)
        {
            using (var scope = preContainer.BeginLifetimeScope())
            {
                var menu = scope.Resolve<IMenuCreator>().Menu;
                while (true)
                {
                    menu.ChooseCategory();
                }
            }
        }

        private static void RegisterActions(ContainerBuilder builder)
        {
            var action = typeof(ConsoleUiAction);
            var actions = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.FullName.Contains("TagsCloudContainer"))
                .GetTypes()
                .Where(t => action.IsAssignableFrom(t))
                .ToArray();
            builder.RegisterTypes(actions).As<ConsoleUiAction>();
            builder.RegisterType<ConsoleMenuCreator>().As<IMenuCreator>();
        }
    }
}
