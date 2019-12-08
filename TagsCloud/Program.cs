using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using NUnit.Framework.Internal.Execution;
using TagsCloud.Interfaces;
using TagsCloud.MenuActions;

namespace TagsCloud
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const string sourceTextFilePath = @"../../text.txt";
            var builder = new ContainerBuilder();
            
            builder.RegisterType<MainForm>().AsSelf();
            builder.RegisterType<PictureBoxImageHolder>()
                .As<IImageHolder, PictureBoxImageHolder>().SingleInstance();
            builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            builder.RegisterType<FontSettings>().AsSelf().SingleInstance();
            builder.RegisterType<Palette>().AsSelf().SingleInstance();
            builder.RegisterType<SpiralSettings>().AsSelf().SingleInstance();
            builder.RegisterType<SaveImageAction>().As<IMenuAction>();
            builder.RegisterType<CircularLayouterAction>().As<IMenuAction>();
            builder.RegisterType<ImageSettingsAction>().As<IMenuAction>();
            builder.RegisterType<FontSettingsAction>().As<IMenuAction>();
            builder.RegisterType<PaletteSettingsAction>().As<IMenuAction>();
            builder.RegisterType<LayoutPainter>().As<ILayoutPainter>();
            builder.Register(c => new Random()).As<Random>();
            builder.RegisterType<LayoutConstructor>().As<ILayoutConstructor>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<ArchimedeSpiral>().As<ISpiral>();
            builder.RegisterType<TagsProcessor>().As<ITagsProcessor>();
            builder.RegisterType<WordsProcessor>().As<IWordsProcessor>();
            builder.RegisterInstance(new TxtReader(sourceTextFilePath)).As<ITextReader>();
            builder.RegisterType<WordLengthFilter>().As<IWordFilter>();
            var container = builder.Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainForm>());
        }
    }
}