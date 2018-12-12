using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using Autofac;
using TagsCloudContainer.BoringWordsGetters;
using TagsCloudContainer.CircularCloudLayouters;
using TagsCloudContainer.Clients;
using TagsCloudContainer.FontSizesChoosers;
using TagsCloudContainer.ImageCreators;
using TagsCloudContainer.ImageSavers;
using TagsCloudContainer.RectanglesFilters;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordsFilters;
using TagsCloudContainer.WordsHandlers;
using TagsCloudContainer.WordsTransformers;

namespace TagsCloudContainer.ProjectSettings
{
    public static class ProjectSettingsGetter
    {
        public static IContainer GetSettings()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces().SingleInstance();
            return builder.Build();
        }
    }
}