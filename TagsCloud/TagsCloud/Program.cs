using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autofac;
using TagsCloud.UiActions;

namespace TagsCloud
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var defaultImageSettings = new ImageSettings(1280, 720);
            var defaultWordsToIgnore = new List<string> {"а", "и", "да", "но", "как", "то", "либо", "ибо", "ну", "у"};

            var builder = new ContainerBuilder();
            builder.RegisterType<PictureBoxImageHolder>().AsSelf().As<IImageHolder>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.Register((a, b) => defaultImageSettings).SingleInstance();
            builder.Register(_ => new WordsFrequencyParser(defaultWordsToIgnore)).As<IWordsFrequencyParser>();

            builder.RegisterType<RenderFileAction>().As<IUiAction>();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<ImageSettingsAction>().As<IUiAction>();
            builder.RegisterType<ParserSettingsAction>().As<IUiAction>();
            builder.RegisterType<MainForm>().AsSelf();
            var container = builder.Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainForm>());
        }
    }
}
