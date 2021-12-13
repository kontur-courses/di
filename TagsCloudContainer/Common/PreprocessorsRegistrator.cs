using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Preprocessors;

namespace TagsCloudContainer.Common
{
    public static class PreprocessorsRegistrator
    {
        public static void RegisterPreprocessors(ContainerBuilder builder)
        {
            var preprocessors = GetActivePreprocessors();
            builder.RegisterTypes(preprocessors).As<IPreprocessor>();
            builder.RegisterType<TagsPreprocessor>().AsSelf();
            DefaultInactivePreprocessorsRegistrator(builder);
        }

        public static Type[] GetActivePreprocessors() 
            => AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.FullName.Contains("TagsCloudContainer"))
                .GetTypes()
                .Where(t => t.IsInstanceOf<IPreprocessor>())
                .ToArray();

        private static void DefaultInactivePreprocessorsRegistrator(ContainerBuilder builder)
        {
            var hashSet = new HashSet<string>();
            CustomTagsFilter.RelevantTag selector = t => true;
            builder.RegisterInstance(hashSet).AsSelf();
            builder.RegisterInstance(selector)
                .As<CustomTagsFilter.RelevantTag>();
        }
    }
}