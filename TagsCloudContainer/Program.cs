using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer.Settings;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Painter;
using TagsCloudContainer.Preprocessing;
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
            var mainForm = (MainForm)null;
            
            try
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<MainForm>().AsSelf().SingleInstance();
                builder.RegisterType<PictureBoxImageHolder>().As<PictureBoxImageHolder, IImageHolder>().SingleInstance();
                builder.RegisterType<ImageSettings>().As<ImageSettings>().SingleInstance();
                builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                    .Where(type => type.GetInterfaces().Contains(typeof(IUiAction))).As<IUiAction>();
                builder.RegisterType<TagCloudPainter>().AsSelf().SingleInstance();
                builder.RegisterType<Palette>().AsSelf().SingleInstance();
                builder.RegisterType<FontSettings>().AsSelf().SingleInstance();
                builder.RegisterType<GradientPainter>().As<ICloudColorPainter>().SingleInstance();
                builder.RegisterType<AppSettings>().As<IFilePathProvider, IImageDirectoryProvider>().SingleInstance();
                builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
                builder.RegisterType<TxtFileReader>().As<IFileReader>().SingleInstance();
                builder.RegisterType<Spiral>().As<IPositionGenerator>();
                builder.RegisterType<SpiralSettings>().AsSelf().SingleInstance();
                //builder.RegisterType<CircularCloudLayouter>().AsSelf();
                builder.Register<Func<ITagCloudLayouter>>(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    return () => new CircularCloudLayouter(context.Resolve<IPositionGenerator>());
                });
                builder.RegisterType<LayouterApplicator>().AsSelf().SingleInstance();
                builder.RegisterType<WordsPreprocessor>().AsSelf().SingleInstance();
                builder.RegisterType<WordsPreprocessorSettings>().AsSelf().SingleInstance();
                mainForm = builder.Build().ResolveOptional<MainForm>();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            Application.Run(mainForm);
        }
    }
}
