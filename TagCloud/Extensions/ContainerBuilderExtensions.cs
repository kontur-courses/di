using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Builder;
using TagCloud.Commands;

namespace TagCloud.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterCommand<T>(this ContainerBuilder builder)
        {
            builder.RegisterType<T>().As<ICommand>().InstancePerLifetimeScope().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}
