using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using TagsCloud2.FrequencyCompiler;
using TagsCloud2.ImageSaver;
using TagsCloud2.InputManager;
using TagsCloud2.Lemmatizer;
using TagsCloud2.Reader;
using TagsCloud2.TagsCloudMaker;
using TagsCloud2.TagsCloudMaker.BitmapMaker;
using TagsCloud2.TagsCloudMaker.Layouter;
using TagsCloud2.TagsCloudMaker.SizeDefiner;
using TagsCloud2.TagsCloudMakerManager;

namespace TagsCloud2;

static class Program
{
    static void Main()
    {
        var mystemExePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Lemmatizer\\mystem.exe");
        var services = ConfigureServices(mystemExePath);
        var serviceProvider = services.BuildServiceProvider();
        var projectManager = serviceProvider.GetService<IProjectManager>();
        projectManager.CollectInformationAndMakePicture();
        //D:\\inputWords.txt
    }

    private static IServiceCollection ConfigureServices(string mystemExePath)
    {
        var services = new ServiceCollection()
            .AddTransient<IWordsReader, Reader.WordsReader>()
            .AddTransient<IImageSaver, ImageSaver.ImageSaver>()
            .AddTransient<IFrequencyCompiler, FrequencyCompiler.FrequencyCompiler>()
            .AddTransient<ISizeDefiner, RectangleSizeDefiner>()
            .AddTransient<ITagsCloudMaker, TagsCloudMaker.TagsCloudMaker>()
            .AddTransient<ILemmatizer>(x =>
                new Lemmatizer.Lemmatizer(mystemExePath))
            .AddTransient<ILayouter>(x => new CircularCloudLayouter(new Point(0, 0)))
            .AddTransient<IBitmapTagsCloudMaker>(x => new BitmapTagsCloudTagsCloudMaker(x.GetRequiredService<ILayouter>()))
            .AddTransient<IInputManager, InputManager.InputManager>()
            .AddTransient<ITagsCloudMakerManager>(x => new TagsCloudMakerManager.TagsCloudMakerManager(x.GetRequiredService<IWordsReader>(),
                x.GetRequiredService<ILemmatizer>(),
                x.GetRequiredService<IFrequencyCompiler>(),
                x.GetRequiredService<IImageSaver>(),
                x.GetRequiredService<ITagsCloudMaker>(),
                x.GetRequiredService<IBitmapTagsCloudMaker>(),
                x.GetRequiredService<ISizeDefiner>()
                ))
            .AddTransient<IProjectManager>(x => new ProjectManager(
                x.GetRequiredService<IInputManager>(),
                x.GetRequiredService<ITagsCloudMakerManager>()));
        return services;
    }
}