using System;
using System.Collections.Generic;
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
            RegisterDefaultCustomFiltersInfo(builder);
            builder.RegisterTypes(preprocessors).As<IPreprocessor>();
            builder.RegisterType<TagsPreprocessor>().AsSelf();
        }

        private static void RegisterDefaultCustomFiltersInfo(ContainerBuilder builder)
        {
            CustomTagsFilter.RelevantTag boringWordsSelector = t => true;
            var boringWords = new HashSet<string>();
            builder.RegisterInstance(boringWordsSelector)
                .As<CustomTagsFilter.RelevantTag>();
            builder.RegisterInstance(boringWords)
                .As<HashSet<string>>();
        }
    }
}