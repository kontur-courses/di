using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Autofac;
using Autofac.Core;
using CloodLayouter.Infrastructer;
using CloudLayouter.App;
using CommandLine;

namespace CloodLayouter.App
{
    public class Bilder
    {
        public IContainer Bild(ParserResult<Options> parserResult)
        {
            
            var assembly = Assembly.GetExecutingAssembly();
            var logicBuilder = new ContainerBuilder();

            parserResult.WithParsed(opt => BuildFileProvider(opt.InputFiles, logicBuilder))
                .WithParsed(opt => BuildSaveDirectoryProvider(opt.OutputFile, logicBuilder));

            //
            logicBuilder.RegisterType<TagCloudDrawer>().As<IDrawer>();
            logicBuilder.RegisterType<ImageSaver>().As<IImageSaver>();
            logicBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            logicBuilder.RegisterType<ImageHolder>().As<IProvider<Bitmap>>().SingleInstance();
            logicBuilder.RegisterType<ImageSettingsProvider>().As<IProvider<ImageSettings>>();         
            //
            logicBuilder.RegisterType<FileWordProvider>().Named<IProvider<IEnumerable<string>>>("FileWordProvider");
            logicBuilder.RegisterType<WordSelector>()
                .As<IWordSlector>()
                .WithParameter(ResolvedParameter.ForNamed<IProvider<IEnumerable<string>>>("FileWordProvider"))
                .Named<IProvider<IEnumerable<string>>>("FromSelectorToConverterProvider");

            logicBuilder.RegisterType<Converter>()
                .WithParameter(
                    ResolvedParameter.ForNamed<IProvider<IEnumerable<string>>>("FromSelectorToConverterProvider"))
                .SingleInstance().As<IProvider<IEnumerable<Tag>>>();

            logicBuilder.RegisterType<ImagePerfomer>();

            return logicBuilder.Build();
        }

        private void BuildFileProvider(IEnumerable<string> filenames, ContainerBuilder builder)
        {
            foreach (var file in filenames)
                builder.RegisterType<StreamProvider>().As<IProvider<StreamReader>>().WithParameter("filename", file);
        }

        private void BuildSaveDirectoryProvider(string filePath, ContainerBuilder builder)
        {
            builder.RegisterType<SaveDirectoryProvider>().As<IProvider<string>>().WithParameter("filePath", filePath);
        }
    }
}