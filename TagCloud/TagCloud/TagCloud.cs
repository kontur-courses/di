using Autofac;
using TagCloud.Extensions;

namespace TagCloud
{
    public class TagCloud
    {
        private IContainer _container;
        private string _filePath;

        public TagCloud(IDrawerSettings drawerSettings, ITextProcessingSettings textProcessingSettings)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(drawerSettings).As<IDrawerSettings>();
            builder.RegisterInstance(textProcessingSettings).As<ITextProcessingSettings>();
            
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<TagCloudDrawer>().As<ITagCloudDrawer>();
            builder.RegisterType<Rose>().As<ISpiral>();
            builder.RegisterType<TextProcessor>().AsSelf();
            builder.RegisterType<WordLayouter>().AsSelf();
            builder.RegisterType<CoordinatesConverter>().AsSelf();
            _container = builder.Build();
        }

        public TagCloud FromFile(string filePath)
        {
            _filePath = filePath;
            return this;
        }

        public void Draw()
        {
            var wordsWithFrequency = _container.Resolve<TextProcessor>().GetInterestingWords(_filePath);
            var layoutedWords = _container.Resolve<WordLayouter>().Layout(wordsWithFrequency);
            _container.Resolve<ITagCloudDrawer>().Draw(layoutedWords).SaveDefault();
        }
    }
}