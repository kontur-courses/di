using System;
using System.Security.Authentication.ExtendedProtection;
using Autofac;
using Autofac.Core;
using TagsCloudPreprocessor;
using TagsCloudPreprocessor.Preprocessors;
using TagsCloudVisualization;

namespace TagCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleClient>()
                .As<IUserInterface>()
                .WithParameter("args", args)
                .SingleInstance();

            builder.RegisterType<XmlWordExcluder>().As<IWordExcluder>();
            builder.RegisterType<DocFileReader>().Keyed<IFileReader>("DocFileReader");
            builder.RegisterType<TxtFileReader>().Keyed<IFileReader>("TxtFileReader");
            builder.RegisterType<TextParser>().As<ITextParser>();
            builder.RegisterType<WordsExcluder>().Named<IPreprocessor>("WordsExcluder");
            builder.RegisterType<WordsStemer>().Named<IPreprocessor>("WordsStemer");

            builder.RegisterType<ArchimedesSpiral>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.Name == "center",
                        (pi, ctx) => ctx.Resolve<IUserInterface>().Config.Center))
                .As<ISpiral>();
            builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();

            builder.RegisterType<TagCloudVisualization>()
                .WithParameters(
                    new[]
                    {
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "font",
                            (pi, ctx) => ctx.Resolve<IUserInterface>().Config.Font),
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "color",
                            (pi, ctx) => ctx.Resolve<IUserInterface>().Config.Color),
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "backgroundColor",
                            (pi, ctx) => ctx.Resolve<IUserInterface>().Config.BackgroundColor),
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "imageExtension",
                            (pi, ctx) => ctx.Resolve<IUserInterface>().Config.ImageExtension)
                    })
                .As<ITagCloudVisualization>();

            builder.RegisterType<TagCloudProgram>()
                .WithParameters(
                    new Parameter[]
                    {
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "config",
                            (pi, ctx) => ctx.Resolve<IUserInterface>().Config),
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "wordsExcluder",
                            (pi, ctx) => ctx.ResolveNamed<IPreprocessor>("WordsExcluder")),
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "wordsStemer",
                            (pi, ctx) => ctx.ResolveNamed<IPreprocessor>("WordsStemer")),
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "fileReader",
                            (pi, ctx) =>
                            {
                                if (ctx.Resolve<IUserInterface>().Config.InputExtension == "txt")
                                    return ctx.ResolveKeyed<IFileReader>("TxtFileReader");
                                if (ctx.Resolve<IUserInterface>().Config.InputExtension == "docx")
                                    return ctx.ResolveKeyed<IFileReader>("DocFileReader");
                                return ctx.ResolveKeyed<IFileReader>("TxtFileReader");
                            }),
                    })
                .AsSelf();
            
            var container = builder.Build();
            
            container.Resolve<TagCloudProgram>().SaveTagCloud();
        }
    }
}