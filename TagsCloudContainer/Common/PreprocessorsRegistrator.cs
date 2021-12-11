using System;
using System.Linq;
using Autofac;
using TagsCloudContainer.Preprocessors;

namespace TagsCloudContainer.Common
{
    public static class PreprocessorsRegistrator
    {
        public static void RegisterPreprocessors(ContainerBuilder builder)
        {
            var preprocessor = typeof(IPreprocessor);
            var preprocessors = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.FullName.Contains("TagsCloudContainer"))
                .GetTypes()
                .Where(t => preprocessor.IsAssignableFrom(t))
                .ToArray();
            builder.RegisterTypes(preprocessors).As<IPreprocessor>();
            builder.RegisterType<TagsPreprocessor>().AsSelf();
        }
    }
}