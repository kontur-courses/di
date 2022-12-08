using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Infrastructure.Algorithm.Curves;
using TagsCloudVisualization.InfrastructureUI;
using TagsCloudVisualization.Painters;


namespace TagsCloudVisualization
{
    public static class Program
    {

        public delegate CloudPainter Factory(DefinerSize definer, ICurve curve);


        [STAThread]
        public static void Main()
        {
            var container = new ContainerBuilder();
            container.RegisterType<CloudForm>();
            container.RegisterType<PictureBoxImageHolder>().As<PictureBoxImageHolder, IImageHolder>().SingleInstance();
            container.Register<Func<DefinerSize, ICurve, CloudPainter>>(c =>
            {
                var holder = c.Resolve<IImageHolder>();
                var analyzer = c.Resolve<IAnalyzer>();
                return (size, curve) => new CloudPainter(holder, analyzer, size, curve);
            }).SingleInstance();
            container.RegisterType<ButterflyCloudAction>().As<IUiAction>();
            container.RegisterType<CircleCloudAction>().As<IUiAction>();
            container.RegisterType<Analyzer>().As<IAnalyzer>().SingleInstance();
            container.RegisterType<ParserTxt>().As<IParser>();
            container.RegisterType<SaveImageAction>().As<IUiAction>().SingleInstance();
            container.RegisterType<SetTextAction>().As<IUiAction>().SingleInstance();
            container.RegisterType<AnalyzerSettings>().AsSelf().SingleInstance();
            var abc = container.Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(abc.Resolve<CloudForm>());
        }
    }

}
