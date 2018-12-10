using Autofac;
using Autofac.Core;
using TagsCloudContainer.Algorithms;
using TagsCloudContainer.DataProviders;
using TagsCloudContainer.ResultFormatters;
using TagsCloudContainer.Settings;
using TagsCloudContainer.SourceTextReaders;
using TagsCloudContainer.TextPreprocessors;

namespace TagsCloudContainer
{
    public class Ioc
    {
        public IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DefaultCloudSettings>().As<ICloudSettings>();
            builder.RegisterType<DefaultSourceFileSettings>().As<ISourceFileSettings>();
            builder.RegisterType<DefaultFontSettings>().As<IFontSettings>();
            builder.RegisterType<TxtSourceTextReader>().As<ISourceTextReader>();
            builder.RegisterType<BasicWordsPreprocessor>().As<IWordsPreprocessor>(); 
            builder.RegisterType<ArchimedeanSpiral>().As<ISpiral>();
            builder.RegisterType<CircularCloudAlgorithm>().As<IAlgorithm>();
            builder.RegisterType<CircularCloudLayouterResultFormatter>().As<IResultFormatter>();
            builder.RegisterType<DataProvider>().As<IDataProvider>();

            return builder.Build();
        }
    }
}
