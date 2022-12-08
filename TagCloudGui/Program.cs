using System;
using Autofac;
using System.Windows.Forms;
using TagCloud.BoringWordsRepositories;
using TagCloud.BoringWordsStorage;
using TagCloud.CloudLayouters;
using TagCloud.PointGenerators;
using TagCloud.Readers;
using TagCloud.TagCloudCreators;
using TagCloud.TagCloudVisualizations;
using TagCloud.WordPreprocessors;

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
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var containers = CreateContainers();
            var preparedContainers = containers.Build();
            Application.Run(preparedContainers.Resolve<MainForm>()); //new MainForm());
        }

        private static ContainerBuilder CreateContainers()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<MainForm>().SingleInstance();

            containerBuilder.RegisterType<SpiralPointGenerator>().As<IPointGenerator>();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            containerBuilder.RegisterType<SingleWordInRowTextFileReader>().As<IReader>();
            containerBuilder.RegisterType<TextFileBoringWordsStorage>().As<IBoringWordsStorage>();
            containerBuilder.RegisterType<SimpleWordPreprocessor>().As<IWordPreprocessor>();
            containerBuilder.RegisterType<WordTagCloudCreator>().As<ITagCloudCreator>();
            containerBuilder.RegisterType<TagCloudVisualizationSettings>().As<ITagCloudVisualizationSettings>().SingleInstance();
            containerBuilder.RegisterType<TagCloudBitmapVisualization>().As<ITagCloudVisualization>();
            return containerBuilder;
        }
    }
}
