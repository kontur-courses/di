using Autofac;
using TagCloud.Utils.Files;
using TagCloud.Utils.Files.Interfaces;
using TagCloud.Utils.Images;
using TagCloud.Utils.Images.Interfaces;

namespace TagCloud.Utils.DI;

public class Configure
{
    public static void ConfigureUtils(ContainerBuilder builder)
    {
        builder
            .RegisterType<FileWordsService>()
            .As<IWordsService>()
            .SingleInstance();

        builder
            .RegisterType<ImageWorker>()
            .As<IImageWorker>()
            .SingleInstance();
    }
}