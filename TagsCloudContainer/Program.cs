using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer.UiActions;
using TagsCloudVisualization;

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
                builder.RegisterType<WordsContainer>().AsSelf().SingleInstance();
                builder.RegisterType<AppSettings>().As<IFilePathProvider, IImageDirectoryProvider>().SingleInstance();
                builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
                builder.RegisterType<TxtFileReader>().As<IFileReader>().SingleInstance();
                builder.RegisterType<Spiral>().As<IPositionGenerator>().SingleInstance();
                builder.RegisterType<TagCloudLayouter>().AsSelf();
                builder.RegisterType<WordDrawInfoGetter>().AsSelf().SingleInstance();
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
