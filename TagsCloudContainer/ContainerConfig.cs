using Autofac;
using NHunspell;
using TagsCloudContainer.Common;
using TagsCloudContainer.Common.Contracts;
using TagsCloudContainer.Common.Filters;

namespace TagsCloudContainer
{
    public static class ContainerConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TextFileReader>().As<IFileReader>();
            builder.RegisterType<TextAnalyzer>().As<ITextAnalyzer>();
            builder.RegisterType<PronounFilter>().As<IWordFilter>();
            builder.RegisterType<PrepositionFilter>().As<IWordFilter>();
            
            const string dictRuAff = @"dicts\ru.aff";
            const string dictRuDic = @"dicts\ru.dic";
            builder.RegisterInstance(new Hunspell(dictRuAff, dictRuDic)).SingleInstance();
            
            return builder.Build();
        }
    }
}