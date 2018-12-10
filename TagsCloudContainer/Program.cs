using Autofac;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Painter;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Settings;
using TagsCloudContainer.UI;

namespace TagsCloudContainer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var builder = new ContainerBuilder();
            RegisterTypes(builder);
            var mainForm = builder.Build().ResolveOptional<UIForm>();
            Application.Run(mainForm);

        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<UIForm>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBoxImageHolder>().As<PictureBoxImageHolder, IImageHolder>().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => type.GetInterfaces().Contains(typeof(IUiAction))).As<IUiAction>();
            builder.RegisterType<TagCloudPainter>().AsSelf().SingleInstance();
            builder.RegisterType<GradientPainter>().As<ICloudColorPainter>().SingleInstance();
            builder.RegisterType<TxtFileReader>().As<IFileReader>().SingleInstance();
            builder.RegisterType<Spiral>().As<IPositionGenerator>();
            builder.RegisterTypes(typeof(Settings.Settings)
                    .GetProperties()
                    .Select(info => info.PropertyType)
                    .ToArray())
                .AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.Register<Func<ITagCloudLayouter>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return () => new CircularCloudLayouter(context.Resolve<IPositionGenerator>());
            });
            builder.RegisterType<LayouterApplicator>().AsSelf().SingleInstance();
            builder.RegisterType<WordsPreprocessor>().AsSelf().SingleInstance();
        }
    }
}
