using System;
using System.Windows.Forms;
using TagsCloudVisualization.App;
using Autofac;

namespace TagsCloudVisualization
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var dependencyBuilder = new DependencyBuilder();
            var container = dependencyBuilder.CreateContainer();
            var form = container.Build().Resolve<MainForm>();
            Application.Run(form);
        }
    }
}
