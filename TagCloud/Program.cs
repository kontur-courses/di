using System;
using System.Reflection;
using System.Windows.Forms;
using TagCloud.Interfaces.GUI;
using GroboContainer.Core;
using GroboContainer.Impl;

namespace TagCloud
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var container = InitializeContainer();
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

        public static Container InitializeContainer()
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            var container = new Container(new ContainerConfiguration(assembly));

            return container;
        }
    }
}
