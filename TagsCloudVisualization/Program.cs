using System;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using NHunspell;
using TagsCloudGenerator.Core.Filters;
using TagsCloudGenerator.Core.Layouters;
using TagsCloudGenerator.Core.Normalizers;
using TagsCloudGenerator.Core.Spirals;
using TagsCloudGenerator.Core.Translators;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Core.Painter;
using TagsCloudVisualization.Infrastructure.Common;
using TagsCloudVisualization.Infrastructure.UiActions;

namespace TagsCloudVisualization
{
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = ConfigureContainer();
            var mainForm = container.ResolveOptional<MainForm>();
            Application.Run(mainForm);
        }

        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainForm>().AsSelf().SingleInstance();

            builder.RegisterType<WordsNormalizer>().As<IWordsNormalizer>().SingleInstance();
            builder.RegisterType<WordsFilter>().As<IWordsFilter>().SingleInstance();
            builder.Register(context =>
                    new Hunspell(@"HunspellDictionaries\ru.aff", @"HunspellDictionaries\ru.dic"))
                .SingleInstance();

            builder.RegisterType<ArchimedeanSpiral>().As<ISpiral>();
            builder.Register<Func<float, float, TextToTagsTranslator>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return (alpha, phi) =>
                    new TextToTagsTranslator(
                        context.Resolve<IWordsNormalizer>(),
                        context.Resolve<Hunspell>(),
                        context.Resolve<IWordsFilter>(),
                        new SpiralRectangleCloudLayouter(new ArchimedeanSpiral(alpha, phi))
                    );
            });
            
            builder.RegisterType<TagCloudSettings>().AsSelf().SingleInstance();
            builder.RegisterType<TagCloudPainter>();
            builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => typeof(IUiAction).IsAssignableFrom(type))
                .AsImplementedInterfaces();

            builder.RegisterType<Palette>().AsSelf().SingleInstance();

            builder.RegisterType<PictureBoxImageHolder>().As<PictureBoxImageHolder, IImageHolder>().SingleInstance();

            return builder.Build();
        }
    }
}