using Autofac;
using Autofac.Builder;
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
            RegisterClient();
            RegisterUserConfig();
            RegisterPaintConfig();
            RegisterCloudLayouter();
            RegisterTextParser();
            RegisterCloudPainter();

            return builder.Build();
        }

        private void RegisterCloudPainter()
        {
            builder.RegisterType<CloudPainter>().AsSelf().SingleInstance();
        }

        private void RegisterTextParser()
        {
            builder.Register(c => new TextParser(
                    c.Resolve<UserConfig>().InputFile,
                    c.Resolve<UserConfig>().ExcludedWords,
                    c.Resolve<UserConfig>().FormatReader))
                .As<ITextParser>().SingleInstance();
        }

        private void RegisterCloudLayouter()
        {
            builder.Register(c => new CircularCloudLayouter(
                    c.Resolve<UserConfig>().ImageCenter))
                .As<ICloudLayouter>().SingleInstance();
        }

        private void RegisterPaintConfig()
        {
            builder.Register(c => new PaintConfig(
                    c.Resolve<UserConfig>().TagsColor,
                    c.Resolve<UserConfig>().TagsFontName,
                    c.Resolve<UserConfig>().TagsFontSize,
                    c.Resolve<UserConfig>().ImageSize))
                .As<IPaintConfig>().SingleInstance();
        }

        private void RegisterUserConfig()
        {
            builder.Register(c => c.Resolve<IClient>().UserConfig)
                .As<UserConfig>();
        }

        private void RegisterClient()
        {
            builder.RegisterInstance(new CommandLineClient(args))
                .As<IClient>();
        }
    }
}
