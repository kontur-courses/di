using System;
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
            //builder.RegisterType<CircularCloudLayouter>().As<CircularCloudLayouter>();
            builder.RegisterType<RectangleForWordsCreator>().As<IRectangleForWordsCreator>();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<DragonFractalAction>().As<IUiAction>();
            builder.RegisterType<CircularCloudLayouterAction>().As<IUiAction>();
            builder.RegisterType<CloudForm>().As<CloudForm>();
            builder.RegisterType<CloudPainterFactory>().As<CloudPainterFactory>();
            builder.RegisterType<CircularCloudLayouterSettings>().As<CircularCloudLayouterSettings>();
            builder.RegisterType<Palette>().AsSelf().SingleInstance();
            //builder.RegisterType<CircularCloudLayouter>();

            builder.RegisterType<CircularCloudLayouter>(); // not a singleton


            //builder.Register(x=>new CircularCloudLayouter(new Point(50,50))).As<ICircularCloudLayouter>();

            builder.RegisterType<XmlObjectSerializer>().As<IObjectSerializer>();

            builder.Register(x => x.Resolve<AppSettings>().ImageSettings).As<ImageSettings>().SingleInstance();

            builder.RegisterType<SettingsManager>().As<SettingsManager>();

            builder.Register(x => x.Resolve<SettingsManager>().Load()).As<AppSettings, IImageDirectoryProvider>().SingleInstance();

            builder.RegisterType<FileBlobStorage>().As<IBlobStorage>();

            builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>().SingleInstance();



            //builder.Register(x => new Point(30, 30)).As<Point>();



            var container = builder.Build();

            //var layouter = container.Resolve<ICircularCloudLayouter>(new TypedParameter(typeof(Point), new Point(40, 40)));
            var words = new Dictionary<string, int>
            {
                {"hello", 3 },
                {"first", 2 },
                {"hell", 1 }
            };
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
