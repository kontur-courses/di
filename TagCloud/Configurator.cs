using System.Drawing;
using Autofac;
using CommandLine;
using TagCloud.AppSettings;
using TagCloud.Drawer;
using TagCloud.FileReader;
using TagCloud.Filter;

namespace TagCloud;

public class Configurator
{
    public static IAppSettings Parse(string[] args, ContainerBuilder builder)
    {
        var settings = Parser.Default.ParseArguments<Settings>(args).WithParsed(o =>
        {
            if (o.UseRandomPalette)
                builder.RegisterType<RandomPalette>().As<IPalette>();
            else
                builder.Register(p =>
                    new CustomPalette(Color.FromName(o.ForegroundColor), Color.FromName(o.BackgroundColor)));
            var filter = new WordFilter().UsingFilter((word) => word.Length > 3);
            if (string.IsNullOrEmpty(o.BoringWordsFile))
                builder.Register(c => filter).As<IFilter>();
            else
            {
                var boringWords = new TxtReader().ReadLines(o.BoringWordsFile);
                builder.Register(c => filter.UsingFilter((word) => !boringWords.Contains(word)));
            }
        });

        return settings.Value;
    }
}