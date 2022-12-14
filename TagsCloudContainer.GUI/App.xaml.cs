using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.Settings;
using TagsCloudContainer.Infrastructure.WordColorProviders.Factories;
using TagsCloudContainer.Infrastructure.WordFontSizeProviders.Factories;
using TagsCloudContainer.Infrastructure.WordLayoutBuilders;
using TagsCloudContainer.Infrastructure.WordPreparers;
using TagsCloudContainer.Infrastructure.WordReaders;

namespace TagsCloudContainer.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.Register(ctx => new Options()).AsSelf().SingleInstance();
            builder.Register(ctx => new GUISettingsProvider(ctx.Resolve<Options>())).As<ISettingsProvider>().SingleInstance();
            builder.Register(ctx => ctx.Resolve<ISettingsProvider>().GetWordColorSettings()).AsSelf();
            builder.Register(ctx => ctx.Resolve<ISettingsProvider>().GetWordFontSettings()).AsSelf();
            builder.Register(ctx => ctx.Resolve<ISettingsProvider>().GetOutputImageSettings()).AsSelf();
            builder.Register(ctx => ctx.Resolve<ISettingsProvider>().GetTextReaderSettings()).AsSelf();

            builder.RegisterType<TextFileReader>().As<IWordReader>().SingleInstance();
            builder.Register(ctx => new WordPreparer(new WordType[] { WordType.Other })).As<IWordPreparer>().SingleInstance();

            builder.RegisterType<WordLinearColorProviderFactory>().As<IWordColorProviderFactory>().SingleInstance();
            builder.RegisterType<WordLinearFontSizeProviderFactory>().As<IWordFontSizeProviderFactory>().SingleInstance();

            builder.RegisterType<CircularWordLayoutBuilder>().As<IWordLayoutBuilder>();
            builder.RegisterType<TagsCloudGenerator>().As<ITagsCloudGenerator>();

            builder.RegisterType<WordPlateVisualizer>().AsSelf().SingleInstance();

            builder.RegisterType<MainWindow>().As<Window>();

            var container = builder.Build();
            container.Resolve<Window>().Show();
        }
    }
}