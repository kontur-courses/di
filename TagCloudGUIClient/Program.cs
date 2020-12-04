using System;
using System.Drawing;
using System.Windows.Forms;
using Autofac;
using CloudLayouters;
using TagCloudCreator;

namespace TagCloud
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var container = GetContainer();
            Application.Run(container.Resolve<Form>());
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Form1>().As<Form>().AsSelf().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<BaseCloudLayouter>().AsSelf();
            builder.RegisterType<RectangleLayouter>().As<BaseCloudLayouter>().AsSelf();
            builder.Register(context => new Point(500, 360));
            builder.RegisterType<CloudPrinter>().AsSelf();
            builder.RegisterType<TxtFileReader>().As<IFileReader>().AsSelf();
            builder.Register(context =>
            {
                var table = new TableLayoutPanel {Dock = DockStyle.Fill};
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
                table.BorderStyle = BorderStyle.None;
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                return table;
            }).AsSelf();
            builder.RegisterType<FullRandomColorSelector>().As<IColorSelector>();
            builder.RegisterType<BlackColorSelector>().As<IColorSelector>();
            builder.RegisterType<RandomFromColorsColorSelector>().As<IColorSelector>();
            builder.RegisterType<TextExtractorBasedReader>().As<IFileReader>();
            return builder.Build();
        }
    }
}