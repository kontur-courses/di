using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace GuiClient
{
    internal static class Program
    {
        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var appServiceProvider = Startup.GetAppServiceProvider();
            var mainForm = appServiceProvider.GetService<MainForm>();

            Application.Run(mainForm);
        }
    }
}