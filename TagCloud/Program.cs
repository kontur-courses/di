using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using TagCloud.Interfaces.GUI;
using GroboContainer.Core;
using GroboContainer.Impl;
using TagCloud.CloudLayouter;
using TagCloud.CloudVisualizer.CloudViewConfiguration;
using TagCloud.FigurePaths;

namespace TagCloud
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var container = new Container(new ContainerConfiguration(Assembly.GetEntryAssembly()));
            container.Configurator.ForAbstraction<ICloudLayouter>().UseType<CircularCloudLayouter>();

            try
            { 
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show(e.Message);
            }
        }
    }
}
