using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using TagsCloudContainer.Preprocessors;

namespace TagsCloudContainer.Common
{
    public static class PreprocessorsRegistrator
    {
        public static void RegisterPreprocessors(ContainerBuilder builder)
        {
            var preprocessors = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.FullName.Contains("TagsCloudContainer"))
                .GetTypes()
                .Where(TypeIsActivePreprocessor)
                .ToArray();
            builder.RegisterTypes(preprocessors).As<IPreprocessor>();
            builder.RegisterType<TagsPreprocessor>().AsSelf();
        }

        private static bool TypeIsActivePreprocessor(Type type)
        {
            var preprocessor = typeof(IPreprocessor);
            return preprocessor.IsAssignableFrom(type) &&
                   type.GetCustomAttribute<StateAttribute>().State == State.Active;
        }
    }
}