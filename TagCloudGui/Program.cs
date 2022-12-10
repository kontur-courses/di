using System;
using System.Windows.Forms;
using Autofac;
using TagCloud.BoringWordsRepositories;
using TagCloud.CloudLayouters;
using TagCloud.PointGenerators;
using TagCloud.Readers;
using TagCloud.TagCloudCreators;
using TagCloud.TagCloudVisualizations;
using TagCloud.WordPreprocessors;
using TagCloudGui.Infrastructure;
using TagCloudGui.Infrastructure.Common;

namespace TagCloudGui
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var containers = CreateContainers();
                var preparedContainers = containers.Build();
                Application.Run(preparedContainers.Resolve<MainForm>());
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static ContainerBuilder CreateContainers()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<SpiralPointGenerator>().As<IPointGenerator>();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            containerBuilder.RegisterType<TextFileBoringWordsStorage>().As<IBoringWordsStorage>();
            containerBuilder.RegisterType<SimpleWordPreprocessor>().As<IWordPreprocessor>();
            containerBuilder.RegisterType<WordTagCloudCreator>().As<ITagCloudCreator>();
            containerBuilder.RegisterType<TagCloudBitmapVisualization>().As<ITagCloudVisualization>();

            containerBuilder.RegisterType<SingleWordInRowTextFileReader>().As<IReader>().SingleInstance();
            containerBuilder.RegisterType<TagCloudVisualizationSettings>().As<ITagCloudVisualizationSettings>().SingleInstance();
            containerBuilder.RegisterType<MainForm>().SingleInstance();
            containerBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).
                AssignableTo<IUiAction>().As<IUiAction>().SingleInstance();
            containerBuilder.RegisterType<PictureBoxImageHolder>().As<IImageHolder>().AsSelf().SingleInstance();

            return containerBuilder;
        }
    }
}
