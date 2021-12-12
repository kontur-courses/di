using System;
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
            var preprocessors = GetActivePreprocessors();
            builder.RegisterTypes(preprocessors).As<IPreprocessor>();
            builder.RegisterType<TagsPreprocessor>().AsSelf();
        }

        public static Type[] GetActivePreprocessors() 
            => AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.FullName.Contains("TagsCloudContainer"))
                .GetTypes()
                .Where(TypeIsActivePreprocessor)
                .ToArray();

        private static bool TypeIsActivePreprocessor(Type type)
        {
            var preprocessor = typeof(IPreprocessor);
            return preprocessor.IsAssignableFrom(type) &&
                   type.GetCustomAttribute<StateAttribute>().State == State.Active;
        }
    }
}