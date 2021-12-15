using System.IO;
using Autofac;
using TagCloud.Drawing;
using TagCloud.Layout;
using TagCloud.TextProcessing;

namespace TagCloud
{
    public class TagCloudBuilder
    {
        private static IContainer? _container;
        private static ContainerBuilder? _builder;

        public TagCloudBuilder GetDefault()
        {
            _builder = new ContainerBuilder();
            _builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            _builder.RegisterType<Drawer>().As<IDrawer>();
            _builder.RegisterType<ArchimedeanSpiral>().As<ICurve>();
            _builder.RegisterType<TextProcessor>().As<ITextProcessor>();
            _builder.RegisterType<WordLayouter>().As<IWordLayouter>();
            _builder.RegisterType<TxtFileProvider>().As<IFileProvider>();
            _builder.RegisterType<SimplePalette>().As<IPalette>();
            _builder.RegisterType<MyStemManager>().AsSelf();
            _builder.RegisterType<TagCloud>().AsSelf();

            return this;
        }

        public TagCloud Build()
        {
            if (_builder == null)
                GetDefault();
            _container = _builder.Build();
            return _container.Resolve<TagCloud>();
        }

        public TagCloudBuilder WithStatusWriter<T>() where T : TextWriter
        {
            if (_builder == null)
                GetDefault();
            var type = typeof(T);
            _builder.RegisterType<T>().As<TextWriter>();
            return this;
        }
    }
}