using System;
using System.Collections.Generic;
using Autofac;
using System.Drawing;
using CommandLine;
using TagsCloudContainer.CloudBuilder;
using TagsCloudContainer.CloudDrawers;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.CloudLayouters.PointGenerators;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.Settings;
using TagsCloudContainer.TextParsers;
using TagsCloudContainer.WordHandler;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ImageSettings imageSettings;
            FileSettings fileSettings;
            TextSettings textSettings;
            
            Parser.Default.ParseArguments<Option>(args).WithParsed(o =>
            {
                imageSettings = new ImageSettings(o.Height, o.Width, o.OutputFile, o.Theme);
                fileSettings = new FileSettings(o.InputFileName);
                textSettings = new TextSettings(o.CountWords);
                
                var builder = new ContainerBuilder();
               
                builder.RegisterInstance(fileSettings).As<FileSettings>();
                builder.RegisterInstance(imageSettings).As<ImageSettings>();
                builder.RegisterInstance(textSettings).As<TextSettings>();
                
                builder.RegisterType<TextFileReader>().As<IFileReader>();
                builder.RegisterType<SimpleWordHandler>().As<IWordHandler>();
                builder.RegisterType<TextParser>().As<ITextParser>();
                builder.RegisterType<TagsCloudBuilder>().As<ICloudBuilder>();
                builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
                builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
                builder.RegisterType<ArchimedesSpiralPointGenerator>().As<IEnumerable<Point>>();
    
                var container = builder.Build();

                var fileReader = container.Resolve<IFileReader>();
                var textParser = container.Resolve<ITextParser>();
                var cloudBuilder = container.Resolve<ICloudBuilder>();
                var cloudDrawer = container.Resolve<ICloudDrawer>();
    
                cloudDrawer.Draw(cloudBuilder.BuildTagsCloud(textParser.Parse(fileReader.Read())));
            });
        }  
    }
}