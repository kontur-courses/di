using Autofac;
using System;
using System.Windows.Forms;

namespace TagCloud
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var container = Builder.BuildContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = container.Resolve<MainForm>();
            Application.Run(mainForm);
        }
    }
}
