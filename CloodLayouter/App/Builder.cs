using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using CloodLayouter.App.Handlers;
using CloodLayouter.Infrastructer;
using CloudLayouter.App;
using CommandLine;

namespace CloodLayouter.App
{
    public class DIBilder
    {
        public IContainer Bild(ParserResult<Options> parserResult)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var logicBuilder = new ContainerBuilder();

            parserResult.WithParsed(opt =>
                logicBuilder.Register(x => new FileWordProvider(opt.InputFiles.ToArray()))
                    .As<IProvider<IEnumerable<string>>>().SingleInstance());

            logicBuilder.RegisterType<WordSelector>().As<IConverter<IEnumerable<string>, IEnumerable<string>>>();
            logicBuilder.RegisterType<FromWordToTagConverter>().As<IConverter<IEnumerable<string>, IEnumerable<Tag>>>();
            logicBuilder.RegisterType<ConvertPerfomer>().As<IProvider<IEnumerable<Tag>>>();
            logicBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();


            parserResult.WithParsed(opt =>
                logicBuilder.Register(x => new ImageSettings(opt.Width, opt.Heigth)).AsSelf().SingleInstance());

            logicBuilder.RegisterType<TagCloudDrawer>().As<IDrawer>();
            logicBuilder.RegisterType<ImageSaver>().As<IImageSaver>(); 
      
            return logicBuilder.Build();
        }


    }
}