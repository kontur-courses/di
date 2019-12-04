using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagCloud.Interfaces.GUI;
using static YandexMystem.Wrapper.Mysteam;
using GroboContainer.Core;
using GroboContainer.Impl;
using TagCloud.CloudLayouter;
using TagCloud.FigurePaths;

namespace TagCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new ContainerConfiguration(Assembly.GetEntryAssembly()));
            container.Configurator.ForAbstraction<ICloudLayouter>().UseType<CircularCloudLayouter>();
            container.Configurator.ForAbstraction<IFigurePath>().UseType<Spiral>();


            try
            { 
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
