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
using TagCloudGui.Actions;
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
            RegisterTagCloudInterfacesIn(containerBuilder);
            RegisterIUiActionsIn(containerBuilder);
            containerBuilder.RegisterType<PictureBoxImageHolder>().As<IImageHolder>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<MainForm>();
            return containerBuilder;
        }


        private static void RegisterTagCloudInterfacesIn(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<SingleWordInRowTextFileReader>().As<IReader>().
                SingleInstance();
            containerBuilder.RegisterType<SingleWordInRowTextFileReader>().As<IBoringWordsReader>();
            containerBuilder.RegisterType<TextFileBoringWordsStorage>().As<IBoringWordsStorage>().
                SingleInstance();
            containerBuilder.RegisterType<SpiralPointGenerator>().As<IPointGenerator>();
            containerBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            containerBuilder.RegisterType<SimpleWordPreprocessor>().As<IWordPreprocessor>();
            containerBuilder.RegisterType<WordTagCloudCreator>().As<ITagCloudCreator>();
            containerBuilder.RegisterType<WordTagCloudBitmapVisualization>().As<ITagCloudVisualization>();
            containerBuilder.RegisterType<TagCloudVisualizationSettings>().As<ITagCloudVisualizationSettings>().
                SingleInstance();
        }

        private static void RegisterIUiActionsIn(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<BoringWordsReadAction>().As<IUiAction>();
            containerBuilder.RegisterType<ImageSettingsAction>().As<IUiAction>();
            containerBuilder.RegisterType<TextFileReadAction>().As<IUiAction>();
            containerBuilder.RegisterType<WordTagCloudCreateAction>().As<IUiAction>();
            containerBuilder.RegisterType<ImageSaveAction>().As<IUiAction>();
        }
    }
}
