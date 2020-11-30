using System;
using System.Collections;
using WeCantSpell.Hunspell;
using System.Windows.Forms;
using Autofac;

namespace TagsCloud
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // TODO здесь по идее должна быть выборка Ui.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
