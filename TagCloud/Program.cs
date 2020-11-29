using System;
using Autofac;
using TagCloud.Infrastructure.Text;
using TagCloud.Infrastructure.Text.Filters;

namespace TagCloud
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create your builder.
            var builder = new ContainerBuilder();
            builder.RegisterType<LineParser>().As<IParser<string>>();
            
            builder.RegisterType<ToLowerFilter>().As<IFilter<string>>();
            builder.RegisterType<InterestingWordsFilter>().As<IFilter<string>>();
            
            
        }
    }
}