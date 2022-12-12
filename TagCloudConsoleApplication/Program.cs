using System;
using System.Drawing;
using Autofac;
using Autofac.Core;
using CommandLine;
using TagCloudConsoleApplication;
using TagCloudPainter;
using TagCloudPainter.Common;
using TagCloudPainter.Interfaces;
using TagCloudPainter.Layouters;
using TagCloudPainter.Parsers;
using TagCloudPainter.Savers;
using TagCloudPainter.Sizers;

namespace TagCloudConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<TagCloudOptions>(args)
                .WithParsed<TagCloudOptions>(o =>
                {
                    var imageSettings = BuildImageSettings(o);
                    var builder = new ContainerBuilder();
                    builder.RegisterType<ImageSettingsProvider>().As<IImageSettingsProvider>().WithProperty("ImageSettings",imageSettings);
                    builder.RegisterType<CloudPainter>().As<ICloudPainter>();
                    builder.RegisterType<ParseSettingsProvider>().As<IParseSettingsProvider>()
                        .WithProperty("ParseSettings", new ParseSettings()).SingleInstance();
                    builder.RegisterType<TxtTagParser>().As<ITagParser>().SingleInstance();
                    builder.RegisterType<WordSizer>().As<IWordSizer>();
                    builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameters(
                        new List<Parameter>
                        {
                            new NamedParameter("center", new Point(o.XCenter, o.YCenter)),
                            new NamedParameter("angleStep", o.Angle),
                            new NamedParameter("radiusStep", o.Radius)
                        });
                    builder.RegisterType<CloudElementBuilder>().As<ITagCloudElementsBuilder>();
                    builder.RegisterType<PngTagCloudSaver>().As<ITagCloudSaver>().SingleInstance();

                    var container = builder.Build();

                    container.Resolve<ITagCloudSaver>().SaveTagCloud(o.OutputPath, o.InputPath);
                });
        }

        private static ImageSettings BuildImageSettings(TagCloudOptions options)
        {
            return new ImageSettings()
            {
                Font = new Font(options.FontFamily, options.FontSize),
                Palette = new Palette()
                {
                    BackgroundColor = Color.FromName(options.BackgroundColor.ToString()),
                    TagsColor = Color.FromName(options.ForegroundColor.ToString())
                },
                Size = new Size(options.ImageWidth, options.ImageHeight)
            };
        }
    }
}