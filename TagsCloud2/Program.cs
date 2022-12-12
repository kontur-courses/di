using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using TagsCloud2.FrequencyCompiler;
using TagsCloud2.ImageSaver;
using TagsCloud2.Lemmatizer;
using TagsCloud2.Manager;
using TagsCloud2.Reader;
using TagsCloud2.TagsCloudMaker;
using TagsCloud2.TagsCloudMaker.BitmapMaker;
using TagsCloud2.TagsCloudMaker.Layouter;
using TagsCloud2.TagsCloudMaker.SizeDefiner;

namespace TagsCloud2;

static class Program
{
    static void Main()
    {
        var mystemExePath = @"D:\шпора-2022\di\TagsCloud2\Lemmatizer\mystem.exe";
        var services = ConfigureSercices(mystemExePath);
        var serviceProvider = services.BuildServiceProvider();
        var manager = serviceProvider.GetService<IManager>();

        manager.Manage();

        //D:\\inputWords.txt
    }

    private static IServiceCollection ConfigureSercices(string mystemExePath)
    {
        var services = new ServiceCollection()
            .AddTransient<IReader, Reader.Reader>()
            .AddTransient<IImageSaver, ImageSaver.ImageSaver>()
            .AddTransient<IFrequencyCompiler, FrequencyCompiler.FrequencyCompiler>()
            .AddTransient<ISizeDefiner, SizeDefiner>()
            .AddTransient<ITagsCloudMaker, TagsCloudMaker.TagsCloudMaker>()
            .AddTransient<ILemmatizer>(x =>
                new Lemmatizer.Lemmatizer(mystemExePath))
            .AddTransient<ILayouter>(x => new CircularCloudLayouter(new Point(0, 0)))
            .AddTransient<IBitmapMaker>(x => new BitmapMaker(x.GetRequiredService<ILayouter>()))
            .AddTransient<IManager>(x => new ConsoleManager(x.GetRequiredService<IReader>(),
                x.GetRequiredService<ILemmatizer>(),
                x.GetRequiredService<IFrequencyCompiler>(),
                x.GetRequiredService<IImageSaver>(),
                x.GetRequiredService<ITagsCloudMaker>(),
                x.GetRequiredService<IBitmapMaker>(),
                x.GetRequiredService<ISizeDefiner>()
            ));
        return services;
    }
}