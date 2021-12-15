using Autofac;
using TagsCloudContainer.Clients;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.PaintConfigs;
using TagsCloudContainer.TextParsers;

namespace TagsCloudContainer.ContainerConfigurers
{
    public class AutofacConfigurer
    {
        private string[] args;
        private ContainerBuilder builder;
        public AutofacConfigurer(string[] args, ContainerBuilder builder)
        {
            this.args = args;
            this.builder = builder;
        }

        public IContainer GetContainer()
        {
            builder.RegisterInstance(new CommandLineClient(args)).As<IClient>();
            builder.Register(c => c.Resolve<IClient>().UserConfig).As<UserConfig>();
            builder.Register(c => new PaintConfig(
                c.Resolve<UserConfig>().TagsColor,
                c.Resolve<UserConfig>().TagsFontName,
                c.Resolve<UserConfig>().TagsFontSize,
                c.Resolve<UserConfig>().ImageSize)).As<IPaintConfig>().SingleInstance();
            builder.Register(c => new CircularCloudLayouter(
                    c.Resolve<UserConfig>().ImageCenter)).As<ICloudLayouter>().SingleInstance();
            builder.Register(c => new TextParser(c.Resolve<UserConfig>().InputFile,
                new BoringWords())).As<ITextParser>().SingleInstance();
            builder.RegisterType<CloudPainter>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
