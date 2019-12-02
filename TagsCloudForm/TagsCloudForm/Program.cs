﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Autofac;
using Autofac.Builder;
using TagsCloudForm.Actions;

namespace TagsCloudForm
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<RectangleForWordsCreator>().As<IRectangleForWordsCreator>();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<CircularCloudLayouterAction>().As<IUiAction>();
            builder.RegisterType<CircularCloudLayouterWithWordsAction>().As<IUiAction>();
            builder.RegisterType<CloudForm>().As<CloudForm>();
            builder.RegisterType<CloudPainterFactory>().As<CloudPainterFactory>();
            builder.RegisterType<CloudWithWordsPainterFactory>().As<CloudWithWordsPainterFactory>();
            builder.RegisterType<CircularCloudLayouterSettings>().As<CircularCloudLayouterSettings>();
            builder.RegisterType<Palette>().AsSelf().SingleInstance();

            builder.RegisterType<CircularCloudLayouter>();



            builder.RegisterType<XmlObjectSerializer>().As<IObjectSerializer>();

            builder.Register(x => x.Resolve<AppSettings>().ImageSettings).As<ImageSettings>().SingleInstance();

            builder.RegisterType<SettingsManager>().As<SettingsManager>();


            builder.Register(x => x.Resolve<SettingsManager>().Load()).As<AppSettings, IImageDirectoryProvider>().SingleInstance();

            builder.RegisterType<FileBlobStorage>().As<IBlobStorage>();

            builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>().SingleInstance();


            var words = new Dictionary<string, int>
            {
                {"hello", 6 },
                {"first", 4 },
                {"hell", 2 },
                {"bingo", 4 },
                {"POLIOMIELIT", 5 }
            };

            builder.Register(x=>words).As<Dictionary<string, int>>().SingleInstance();

            var container = builder.Build();

            //var rects = container.Resolve<RectangleForWordsCreator>().CreateRectanglesForWords(words);
            var form = container.Resolve<CloudForm>(
                new TypedParameter(typeof(IContainer), container),
                new NamedParameter("rectanglesNum", 30),
                new NamedParameter("minRectSize", 10),
                new NamedParameter("maxRectSize", 30)
            );

            //var form = new CloudForm(container, 30, 10, 30)
            //{
            //    Size = new Size(600, 600)
            //};

            Application.Run(form);
        }
    }
}
