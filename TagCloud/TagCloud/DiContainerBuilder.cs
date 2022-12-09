using Autofac;
using TagsCloudLayouter;

namespace TagCloud;

public class DiContainerBuilder
{
    public IContainer Build()
    {
        var builder = new ContainerBuilder();

        builder.RegisterInstance(new TxtFileLoader()).As<IFileLoader>();
        
        builder.RegisterInstance(new Palette(Color.Tan, Color.Teal)).AsSelf();
        builder.RegisterInstance(new SizeProperties(new Size(1024, 1024))).AsSelf();
        builder.RegisterInstance(new FontProperties()).AsSelf();
        
        builder.RegisterType<WordsParser>().As<IWordsParser>();
        builder.RegisterType<FrequencyDictionary>().AsSelf();
        builder.RegisterInstance(new CircularCloudLayouter(new Point(512, 512), 0.1, 0.1)).As<ICloudLayouter>();
        builder.RegisterType<TextWrapper>().AsSelf();
        builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
        return builder.Build();
    }
}