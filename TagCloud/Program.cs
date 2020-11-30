using System;
using System.IO;
using Autofac;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text;
using TagCloud.Infrastructure.Text.Filters;

namespace TagCloud
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LineParser>().As<IParser<string>>();
            
            builder.RegisterType<ToLowerFilter>().As<IFilter<string>>();
            var fileName = "mystem";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            builder.RegisterType<InterestingWordsFilter>()
                .As<IFilter<string>>()
                .WithParameter(new TypedParameter(typeof(string), path));
            builder.RegisterType<Settings>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            var container = builder.Build();
            var settingsFactory = container.Resolve<Func<Settings>>();
            settingsFactory().ExcludedTypes = new []{"CONJ", "SPRO"};
        }
    }
}