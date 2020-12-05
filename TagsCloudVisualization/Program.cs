using System;
using TagsCloudVisualization.Canvases;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.FormAction;
using TagsCloudVisualization.PointsGenerators;
using TagsCloudVisualization.TagCloudBuilders;
using TagsCloudVisualization.TagCloudLayouter;
using TagsCloudVisualization.TagCloudVisualizers;

namespace TagsCloudVisualization
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ICloudLayouter, CircularCloudLayouter>();
            services.AddSingleton<ICloudVisualizer, TagCloudVisualizer>();
            services.AddSingleton<ITagCloudBuilder, TagCloudBuilder>();
            services.AddSingleton<ICanvas, Canvas>();

            services.AddSingleton<SpiralParams>();
            services.AddSingleton<FontSettings>();
            services.AddSingleton<ImageSettings>();
            services.AddSingleton<PaintingSettings>();
            services.AddSingleton<ForbiddenWordsSettings>();

            services.AddSingleton<IPointGenerator, ArchimedesSpiral>(x =>
                new ArchimedesSpiral(x.GetService<SpiralParams>(), x.GetService<ICanvas>().GetImageCenter()));
            
            services.AddTransient<IFormAction, SaveImageAction>();
            services.AddTransient<IFormAction, CloudLayouterAction>();
            services.AddTransient<IFormAction, FontAction>();
            services.AddTransient<IFormAction, PaletteAction>();
            services.AddTransient<IFormAction, ImageSettingsAction>();
            services.AddTransient<IFormAction, ExampleAction>();
            services.AddTransient<IFormAction, ForbiddenWordsAction>();

            services.AddSingleton<Form, MainForm>();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(services.BuildServiceProvider().GetService<Form>());
        }
    }
}