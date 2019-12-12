using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using TagCloud.Interfaces.GUI;
using GroboContainer.Core;
using GroboContainer.Impl;
using TagCloud.CloudLayouter;
using TagCloud.WordsPreprocessing;
using TagCloud.WordsPreprocessing.TextAnalyzers;

namespace TagCloud
{
    public class Program
    {[STAThread]
        static void Main(string[] args)
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
            container.Configurator.ForAbstraction<ICloudLayouter>().UseType<CircularCloudLayouter>();
            container.Configurator.ForAbstraction<HashSet<string>>().UseInstances(new HashSet<string>());
            container.Configurator.ForAbstraction<HashSet<SpeechPart>>().UseInstances(new HashSet<SpeechPart>());

            return container;
        }
    }
}
